using AutoMapper;
using Domain.Entities;
using DomainServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using NuGet.Protocol.Plugins;
using System.Net;
using Web.Models;

namespace Web.Controllers
{
    public class BoardGameNightController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IUserRepository _userRepository;
        private readonly IBoardGameNightRepository _boardGameNightRepository;
        private readonly IBoardGameRepository _boardGameRepository;
        private readonly IFoodAndDrinksPrefRepository _foodAndDrinksPrefRepository;

        public BoardGameNightController(
            UserManager<IdentityUser> userManager,
            IMapper mapper,
            IToastNotification toastNotification,
            IUserRepository userRepository,
            IBoardGameNightRepository boardGameNightRepository,
            IBoardGameRepository boardGameRepository,
            IFoodAndDrinksPrefRepository foodAndDrinksPrefRepository
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _userRepository = userRepository;
            _boardGameNightRepository = boardGameNightRepository;
            _boardGameRepository = boardGameRepository;
            _foodAndDrinksPrefRepository = foodAndDrinksPrefRepository;
        }

        public IActionResult Hosting()
        {
            var successMessage = TempData["SuccessMessage"];

            // Check if there is a temporary success message in TempData
            if (successMessage is not null)
            {
                // Show the success message using TempData
                _toastNotification.AddSuccessToastMessage(successMessage.ToString());
            }

            // Get the current user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!)!;

            // Get all board game nights hosted by the current user
            var boardGameNights = _boardGameNightRepository.GetAllHostingBoardGameNightsForUser(user.Id);

            var boardGameNightVMs = _mapper.Map<List<BoardGameNightViewModel>>(boardGameNights);

            return View(boardGameNightVMs);
        }

        public IActionResult Create()
        {
            // Get the current user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (user == null)
            {
                // If the user is null, go to login page
                return RedirectToAction("Login", "Account");
            }

            // Create a new BoardGameNightViewModel
            BoardGameNightViewModel boardGameNightVM = new()
            {
                Address = user.Address,
                City = user.City,
                FoodAndDrinksPreference = new()
            };

            return View(boardGameNightVM);
        }

        [HttpPost]
        public IActionResult Create(BoardGameNightViewModel boardGameNightVM)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                return View(boardGameNightVM);
            }

            // Get the current user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (user == null)
            {
                // If the user is null, go to login page
                return RedirectToAction("Login", "Account");
            }

            var boardGameNight = _mapper.Map<BoardGameNight>(boardGameNightVM);
            boardGameNight.Host = user;

            // Add the boardGameNight to the database
            _boardGameNightRepository.CreateBoardGameNight(boardGameNight);

            // Set a success message in TempData
            TempData["SuccessMessage"] = "Board game night created successfully!";

            // Redirect to the overview page
            return RedirectToAction("Overview");

        }

        public IActionResult Details(int boardGameNightId)
        {
            // Get the board game night from the database
            var boardGameNight = _boardGameNightRepository.GetBoardGameNightById(boardGameNightId);

            // Check if the board game night exists
            if (boardGameNight == null)
            {
                return NotFound();
            }

            var isHost = boardGameNight.Host!.Email == User.Identity!.Name!;
            var isAttending = boardGameNight.Attendees!.Any(a => a.Email == User.Identity!.Name!);

            // Map the board game night to a BoardGameNightViewModel
            var boardGameNightVM = _mapper.Map<BoardGameNightViewModel>(boardGameNight);

            boardGameNightVM.IsHost = isHost;
            boardGameNightVM.IsAttending = isAttending;

            // Display success or error message
            var errorMessage = TempData["ErrorMessage"];
            var successMessage = TempData["SuccessMessage"];

            if (errorMessage is not null)
            {
                _toastNotification.AddErrorToastMessage(errorMessage.ToString());
            } else if (successMessage is not null)
            {
                _toastNotification.AddSuccessToastMessage(successMessage.ToString());
            }

            return View(boardGameNightVM);
        }

        public IActionResult Edit(int boardGameNightId)
        {
            // Get the board game night from the database
            var boardGameNight = _boardGameNightRepository.GetBoardGameNightById(boardGameNightId);
            if (boardGameNight == null)
            {
                return NotFound();
            }

            // Get the current user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (user == null)
            {
                // If the user is null, go to login page
                return RedirectToAction("Login", "Account");
            }

            // Check if the current user is the host of the board game night
            if (boardGameNight.Host!.Id != user.Id)
            {
                return Unauthorized();
            }

            // Map the board game night to a BoardGameNightViewModel
            var boardGameNightVM = _mapper.Map<BoardGameNightViewModel>(boardGameNight);

            return View(boardGameNightVM);
        }

        [HttpPost]
        public IActionResult Edit(BoardGameNightViewModel boardGameNightVM)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                return View(boardGameNightVM);
            }

            // Get the board game night from the database
            var boardGameNight = _boardGameNightRepository.GetBoardGameNightById(boardGameNightVM.Id);

            // Check if the board game night exists
            if (boardGameNight == null)
            {
                return NotFound();
            }

            // Check if there are attendees, if so, redirect back to the details page
            if (boardGameNight.Attendees!.Count > 0)
            {
                TempData["ErrorMessage"] = "Can't save changes, someone has joined the board game night";
                return RedirectToAction("Details", new { boardGameNightId = boardGameNight.Id });
            }


            // Get the current user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (user == null)
            {
                // If the user is null, go to login page
                return RedirectToAction("Login", "Account");
            }

            // Check if the current user is the host of the board game night
            if (boardGameNight.Host!.Id != user.Id)
            {
                return Unauthorized();
            }

            var host = boardGameNight.Host;

            // Map the new values to the board game night
            boardGameNight = _mapper.Map(boardGameNightVM, boardGameNight);

            // Set the host to the original host
            boardGameNight.Host = host;
            boardGameNight.HostId = host.Id;

            // Update the board game night in the database
            _boardGameNightRepository.UpdateBoardGameNight(boardGameNight);

            // Set a success message in TempData
            TempData["SuccessMessage"] = "Board game night updated successfully!";

            // Redirect back to the details page
            return RedirectToAction("Details", new { boardGameNightId = boardGameNight.Id });
        }

        [HttpPost]
        public IActionResult Delete(BoardGameNightViewModel boardGameNightVM)
        {
            // Get the board game night from the database
            var boardGameNight = _boardGameNightRepository.GetBoardGameNightById(boardGameNightVM.Id);

            // Check if the board game night exists
            if (boardGameNight == null)
            {
                return NotFound();
            }

            // Check if there are attendees, if so, redirect back to the details page
            if (boardGameNight.Attendees!.Count > 0)
            {
                TempData["ErrorMessage"] = "Can't delete board game night, someone has joined";
                return RedirectToAction("Details", new { boardGameNightId = boardGameNight.Id });
            }


            // Get the current user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (user == null)
            {
                // If the user is null, go to login page
                return RedirectToAction("Login", "Account");
            }

            // Check if the current user is the host of the board game night
            if (boardGameNight.Host!.Id != user.Id)
            {
                return Unauthorized();
            }

            // Update the board game night in the database
            _boardGameNightRepository.DeleteBoardGameNight(boardGameNight);

            // Set a success message in TempData
            TempData["SuccessMessage"] = "Board game night deleted successfully!";

            // Redirect back to the overview page
            return RedirectToAction("Hosting");
        }
    }
}
