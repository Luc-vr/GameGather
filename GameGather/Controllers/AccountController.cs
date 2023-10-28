using AutoMapper;
using Domain.Entities;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Web.Models;

namespace Web.Controllers;
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IToastNotification _toastNotification;
    private readonly IUserRepository _userRepository;
    private readonly IFoodAndDrinksPrefRepository _foodAndDrinksPrefRepository;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IMapper mapper,
        IToastNotification toastNotification,
        IUserRepository userRepository,
        IFoodAndDrinksPrefRepository foodAndDrinksPrefRepository
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _toastNotification = toastNotification;
        _userRepository = userRepository;
        _foodAndDrinksPrefRepository = foodAndDrinksPrefRepository;
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel registerVM)
    {

        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            ModelState.AddModelError("", "Already signed in.");
        }
        if (!ModelState.IsValid)
        {
            return View();
        }

        // Check if the email is already in use
        var userExists = await _userManager.FindByEmailAsync(registerVM.Email);

        if (userExists != null)
        {
            ModelState.AddModelError("Email", "Email already in use.");
            return View();
        }

        // First, create a new IdentityUser
        var user = new IdentityUser(registerVM.Email)
        {
            Email = registerVM.Email,
            NormalizedEmail = registerVM.Email?.ToUpper()
        };

        var registerResult = await _userManager.CreateAsync(user, registerVM.Password);

        if (!registerResult.Succeeded)
        {
            foreach (var error in registerResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            _toastNotification.AddErrorToastMessage("Registration failed.");
            return View();
        }

        // If the identity user was created successfully, create a new User entity
        var newUser = _mapper.Map<User>(registerVM);
        newUser.FoodAndDrinksPreference = new FoodAndDrinksPreference();

        // Add the new user to the database
        _userRepository.CreateUser(newUser);

        // Registration successful, set temporary success message for the toast notification
        TempData["SuccessMessage"] = "Registration successful! Please log in.";

        return RedirectToAction(nameof(Login), new { returnUrl = "/Home/Index" });
    }

    [AllowAnonymous]
    public IActionResult Login(string? ReturnUrl)
    {
        // Check if user is already logged in
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        var successMessage = TempData["SuccessMessage"];

        // Check if there is a temporary success message in TempData
        if (successMessage is not null)
        {
            // Show the success message using TempData
            _toastNotification.AddSuccessToastMessage(successMessage.ToString());
        }

        // If returnUrl is null, set it to the root
        ReturnUrl ??= "/";

        // Create a new LoginViewModel and set the ReturnUrl
        LoginViewModel loginVM = new()
        {
            ReturnUrl = ReturnUrl
        };

        return View(loginVM);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (ModelState.IsValid)
        {
            var user =
                await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result =
                    await _signInManager.PasswordSignInAsync(user,
                                           loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    // Redirect to returnUrl if it is set
                    if (!string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return Redirect(loginVM.ReturnUrl);
                    }
                    // Redirect to home page if returnUrl is not set
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }

        ModelState.AddModelError("", "Invalid email or password");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        TempData["SuccessMessage"] = "Signed out";

        // Go back to login page
        return RedirectToAction(nameof(Login));
    }

    public IActionResult Preferences()
    {
        // Get the preferences of the current user
        var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

        if (user == null)
        {
            // If the user is null, go to login page
            return RedirectToAction(nameof(Login));
        }

        var preferences = user.FoodAndDrinksPreference;

        // Create a new UserPreferencesViewModel and set the preferences
        if (preferences == null)
        {
            return View(new UserPreferencesViewModel());
        }

        var userPreferencesVM = _mapper.Map<UserPreferencesViewModel>(preferences);

        return View(userPreferencesVM);
    }

    [HttpPost]
    public IActionResult Preferences(UserPreferencesViewModel userPreferencesVM)
    {
        // Get the preferences of the current user
        var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

        if (user == null)
        {
            // If the user is null, go to login page
            return RedirectToAction(nameof(Login));
        }

        var preferences = user.FoodAndDrinksPreference;

        // If the preferences are null, create a new FoodAndDrinksPreference
        preferences ??= new FoodAndDrinksPreference();

       preferences = _mapper.Map(userPreferencesVM, preferences);

        // Update the preferences in the database
        _foodAndDrinksPrefRepository.UpdateFoodAndDrinksPref(preferences);

        _toastNotification.AddSuccessToastMessage("Preferences updated");

        return View();
    }

    public IActionResult Profile()
    {

        // Get the current user
        var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

        // Create a new UserPreferencesViewModel and set the preferences
        if (user == null)
        {
            // If the user is null, go to login page
            return RedirectToAction(nameof(Login));
        }

        var profileVM = _mapper.Map<ProfileViewModel>(user);

        return View(profileVM);
    }

    [HttpPost]
    public IActionResult Profile(ProfileViewModel profileVM)
    {
        if (!ModelState.IsValid)
        {
            _toastNotification.AddErrorToastMessage("Profile not updated");
            return View();
        }

        // Get the current user
        var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

        // Create a new UserPreferencesViewModel and set the preferences
        if (user == null)
        {
            // If the user is null, go to login page
            return RedirectToAction(nameof(Login));
        }

        user = _mapper.Map(profileVM, user);

        // Update the user in the database
        _userRepository.UpdateUser(user);

        // Send notification to the user
        _toastNotification.AddSuccessToastMessage("Profile updated successfully");

        return View();
    }


}
