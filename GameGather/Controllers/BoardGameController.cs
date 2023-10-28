using AutoMapper;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class BoardGameController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoardGameRepository _boardGameRepository;

        public BoardGameController(IMapper mapper, IBoardGameRepository boardGameRepository)
        {
            _mapper = mapper;
            _boardGameRepository = boardGameRepository;
        }

        public IActionResult Index()
        {
            // Get all board games
            var boardGames = _boardGameRepository.GetAllBoardGames();

            // Map board games to view models
            var boardGameViewModels = _mapper.Map<ICollection<BoardGameViewModel>>(boardGames);

            return View(boardGameViewModels);
        }

        public IActionResult Details(int boardGameId)
        {
            // Get board game by boardGameNightId
            var boardGame = _boardGameRepository.GetBoardGameById(boardGameId);

            // Map board game to view model
            var boardGameViewModel = _mapper.Map<BoardGameViewModel>(boardGame);

            return View(boardGameViewModel);
        }
    }
}
