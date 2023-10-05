using AutoMapper;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Web.Models;

namespace Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IUserRepository _userRepository;
        private readonly IBoardGameNightRepository _boardGameNightRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IUserRepository userRepository, IToastNotification toastNotification, IBoardGameNightRepository boardGameNightRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _boardGameNightRepository = boardGameNightRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        public IActionResult Overview()
        {
            // Get the user and reviews from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!)!;
            var reviews = _reviewRepository.GetReviewsForBoardGameNightsHostedByUser(user.Id);

            // Get the amount of board game nights hosted by the user
            var boardGameNightsHostedByUser = _boardGameNightRepository.GetAllPastHostingBoardGameNightsForUser(user.Id);

            // Map the reviews to the view model
            var reviewVMs = _mapper.Map<IEnumerable<ReviewViewModel>>(reviews);

            var reviewOverviewVM = new ReviewOverviewViewModel
            {
                Reviews = reviewVMs,
                NumberOfHostedBoardGameNights = boardGameNightsHostedByUser.Count
            };

            return View(reviewOverviewVM);
        }

        public IActionResult UnreviewedBGN()
        {
            // Get the user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!)!;

            // Get the board game nights where the user did not write a review for
            var boardGameNights = _boardGameNightRepository.GetBoardGameNightsWithoutUserReview(user.Id);

            // Map the board game nights to the view model
            var boardGameNightVMs = _mapper.Map<IEnumerable<BoardGameNightViewModel>>(boardGameNights);

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

            return View(boardGameNightVMs);
        }

        public IActionResult Write(int boardGameNightId)
        {
            // Get the user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!)!;

            // Get the board game night from the database
            var boardGameNight = _boardGameNightRepository.GetBoardGameNightById(boardGameNightId);

            // Create review view model
            var reviewVM = new ReviewViewModel
            {
                BoardGameNightId = boardGameNight!.Id,
                UserId = user.Id,
            };

            return View(reviewVM);
        }

        [HttpPost]
        public IActionResult Write(ReviewViewModel reviewVM)
        {
            // Get the user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!)!;

            // If user is not logged in, redirect to login page
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get the board game night from the database
            var boardGameNight = _boardGameNightRepository.GetBoardGameNightById(reviewVM.BoardGameNightId);

            // Convert the board game night to the view model
            //var boardGameNightVM = _mapper.Map<BoardGameNightViewModel>(boardGameNight);

            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                return View(reviewVM);
            }

            // Create review
            var review = _mapper.Map<Domain.Entities.Review>(reviewVM);
            review.User = user;
            review.BoardGameNight = boardGameNight;

            // Add the review to the database
            _reviewRepository.CreateReview(review);

            // Display success message
            TempData["SuccessMessage"] = "Review successfully written!";

            return RedirectToAction("UnreviewedBGN");
        }
    }
}
