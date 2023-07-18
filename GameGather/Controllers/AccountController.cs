using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Models;

namespace Web.Controllers;
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        var model = new LoginViewModel();
        return View();
    }

    /// <summary>
    /// TODO: separate Login and Register VM's
    /// </summary>
    /// <param name="loginVM"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(LoginViewModel loginVM)
    {

        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            ModelState.AddModelError("", "Already signed in.");
        }
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = new IdentityUser(loginVM.Email);
        var registerResult = await _userManager.CreateAsync(user, loginVM.Password);

        if (registerResult.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim("UserType", "user"));

            return RedirectToAction(nameof(Login), new { returnUrl = "/Guest/" });
        }

        foreach (var error in registerResult.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        return View();
    }

    [AllowAnonymous]
    public IActionResult Login(string ReturnUrl)
    {
        // Check if user is already logged in
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        // If returnUrl is null, set it to the root
        ReturnUrl ??= "/";

        return View(new LoginViewModel { ReturnUrl = ReturnUrl });
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

        ModelState.AddModelError("", "Invalid name or password");
        return View();
    }

    public IActionResult Details(LoginViewModel loginVm)
    {

        if (User != null)
        {
            return View(loginVm);
        }

        return RedirectToAction("Index", "Home");
    }


    public async Task<RedirectResult> Logout(string returnUrl = "/")
    {
        await _signInManager.SignOutAsync();
        return Redirect(returnUrl);
    }

    public async Task<IActionResult> AccessDenied(string returnUrl)
    {
        return View();
    }


}
