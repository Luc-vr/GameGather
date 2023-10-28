using Domain.Entities;
using DomainServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGatherRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        private readonly IBoardGameRepository _boardGameRepository;

        public BoardGameController(IBoardGameRepository boardGameRepository)
        {
            _boardGameRepository = boardGameRepository;
        }

        [HttpGet]
        public ActionResult<List<BoardGame>> Get()
        {
            return Ok(_boardGameRepository.GetAllBoardGames().ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<BoardGame> Get(int id)
        {
            var result = _boardGameRepository.GetBoardGameById(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
