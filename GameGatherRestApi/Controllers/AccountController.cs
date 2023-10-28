using GameGatherRestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DomainServices;
using Domain.Enums;

namespace GameGatherRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 IUserRepository userRepository,
                                 IMapper mapper,
                                 IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationCredentials authenticationCredentials)
        {
            var user = await _userManager.FindByNameAsync(authenticationCredentials.Email);
            if (user != null)
            {
                if ((await _signInManager.PasswordSignInAsync(user,
                    authenticationCredentials.Password, false, false)).Succeeded)
                {
                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
                        Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["BearerTokens:ExpiryMinutes"]!)),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["BearerTokens:Key"]!)), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);

                    return Ok(new { Succes = true, Token = handler.WriteToken(securityToken) });
                }
            }

            return BadRequest();
        }

        [HttpGet("profile")]
        public IActionResult GetUserProfile()
        {
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);            

            if (user == null)
            {
                return NotFound("User not found");
            }

            var profileDTO = _mapper.Map<UserProfileDTO>(user);

            return Ok(profileDTO);
        }

        [HttpPost("profile")]
        public IActionResult UpdateUserProfile([FromBody] UserProfileDTO updatedProfile)
        {
            // Get the user by email from the repository
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Convert the string representation of gender to the Gender enum
            if (Enum.TryParse(updatedProfile.Gender, out Gender parsedGender))
            {
                user.Gender = parsedGender;
            }
            else
            {
                return BadRequest("Invalid gender value");
            }

            if (!user.Email!.Equals(updatedProfile.Email))
            {
                return BadRequest("Not allowed to alter email");
            }

            // Update the user's profile with the data from the request
            user.FirstName = updatedProfile.FirstName;
            user.LastName = updatedProfile.LastName;
            user.BirthDate = updatedProfile.BirthDate;
            user.City = updatedProfile.City;
            user.Address = updatedProfile.Address;

            // Add validation to ensure the BirthDate is before today
            if (user.BirthDate >= DateTime.Today)
            {
                return BadRequest("BirthDate must be before today.");
            }

            // Save the updated user profile
            _userRepository.UpdateUser(user);

            return Ok("User profile updated successfully");
        }

        [HttpGet("preferences")]
        public IActionResult GetUserPreferences()
        {
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var preferencesDTO = new UserPreferenceDTO
            {
                LactoseFree = user.FoodAndDrinksPreference!.LactoseFree,
                NutFree = user.FoodAndDrinksPreference!.NutFree,
                AlcoholFree = user.FoodAndDrinksPreference!.AlcoholFree,
                Vegetarian = user.FoodAndDrinksPreference!.Vegetarian
            };

            return Ok(preferencesDTO);
        }

        [HttpPost("preferences")]
        public IActionResult UpdateUserPreferences([FromBody] UserPreferenceDTO updatedPreferences)
        {
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update the user's preferences with the data from the request
            user.FoodAndDrinksPreference!.LactoseFree = updatedPreferences.LactoseFree;
            user.FoodAndDrinksPreference!.NutFree = updatedPreferences.NutFree;
            user.FoodAndDrinksPreference!.AlcoholFree = updatedPreferences.AlcoholFree;
            user.FoodAndDrinksPreference!.Vegetarian = updatedPreferences.Vegetarian;

            // Save the updated user preferences
            _userRepository.UpdateUser(user);

            return Ok("User preferences updated successfully");
        }
    }
}
