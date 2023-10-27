using AutoMapper;
using Domain.Entities;
using DomainServices;
using GameGatherRestApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GameGatherRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameNightController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IBoardGameNightRepository _boardGameNightRepository;
        private readonly IBoardGameRepository _boardGameRepository;
        private readonly IFoodAndDrinksPrefRepository _foodAndDrinksPrefRepository;

        public BoardGameNightController(
            IMapper mapper,
            IUserRepository userRepository,
            IBoardGameNightRepository boardGameNightRepository,
            IBoardGameRepository boardGameRepository,
            IFoodAndDrinksPrefRepository foodAndDrinksPrefRepository
            )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _boardGameNightRepository = boardGameNightRepository;
            _boardGameRepository = boardGameRepository;
            _foodAndDrinksPrefRepository = foodAndDrinksPrefRepository;
        }

        // UNIMPLEMENTED
        //[HttpGet]
        //public ActionResult<BoardGameNight> Get()
        //{
        //    var result = _boardGameNightRepository.GetAllBoardGameNights();
        //
        //    if (result != null)
        //    {
        //          var boardGameNightDTO = result.Select(bgn => _mapper.Map<BoardGameNightDTO>(bgn)).ToList();
        //
        //          return Ok(boardGameNightDTO);
        //    }
        //
        //    return NotFound();
        //}

        [HttpGet("{id}")]
        public ActionResult<BoardGameNight> Get(int id)
        {
            var result = _boardGameNightRepository.GetBoardGameNightById(id);

            if (result != null)
            {
                var boardGameNightDTO = _mapper.Map<BoardGameNightDTO>(result);

                return Ok(boardGameNightDTO);
            }

            return NotFound();
        }

        [HttpGet("hosted")]
        public ActionResult<BoardGameNight> GetHosting()
        {
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            var result = _boardGameNightRepository.GetAllPastHostingBoardGameNightsForUser(user.Id);

            if (result != null)
            {
                var boardGameNightDTOs = result.Select(bgn => _mapper.Map<BoardGameNightDTO>(bgn)).ToList();

                return Ok(boardGameNightDTOs);
            }

            return NotFound();
        }

        [HttpGet("hosts")]
        public ActionResult<BoardGameNight> GetAllHosts()
        {
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            var result = _boardGameNightRepository.GetAllHostingBoardGameNightsForUser(user.Id);

            if (result != null)
            {
                var boardGameNightDTOs = result.Select(bgn => _mapper.Map<BoardGameNightDTO>(bgn)).ToList();

                return Ok(boardGameNightDTOs);
            }

            return NotFound();
        }

        [HttpGet("upcoming")]
        public ActionResult<BoardGameNight> GetUpcoming()
        {
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            var result = _boardGameNightRepository.GetAllUpcomingHostingBoardGameNightsForUser(user.Id);

            if (result != null)
            {
                var boardGameNightDTOs = result.Select(bgn => _mapper.Map<BoardGameNightDTO>(bgn)).ToList();

                return Ok(boardGameNightDTOs);
            }

            return NotFound();
        }


        [HttpPost("{id}/join")]
        public IActionResult Join(int id)
        {
            // Get the board game night from the database
            var boardGameNight = _boardGameNightRepository.GetBoardGameNightById(id);

            // Get the current user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (boardGameNight == null || user == null)
            {
                return NotFound();
            }

            // Check if the board game night is full
            if (boardGameNight.Attendees!.Count >= boardGameNight.MaxAttendees)
            {
                return Forbid("Board game night is already full");
            }

            // Check if the user is 18 or older and the board game night is 18+
            if (user.Age < 18 && boardGameNight.IsAdultOnly)
            {
                return Forbid("This board game night is for adults only");
            }

            _boardGameNightRepository.AttendBoardGameNight(user.Id, boardGameNight.Id);
           
            return Ok(new { Succes = true, Message = "You have joined the board game night!" });
        }

        [HttpPost("{id}/unjoin")]
        public IActionResult Unjoin(int id)
        {
            // Get the board game night from the database
            var boardGameNight = _boardGameNightRepository.GetBoardGameNightById(id);

            // Get the current user from the database
            var user = _userRepository.GetUserByEmail(User.Identity!.Name!);

            if (boardGameNight == null || user == null)
            {
                return NotFound();
            }

            // UNIMPLEMENTED
            //// Check if the user is already attending the board game night
            //if (_boardGameNightRepository.GetAllAttendeesForBoardGameNight(boardGameNight.Id).Select(u => u.Id).Contains(user.Id))
            //{
            //    return BadRequest("You are not currently attending this board game night.");
            //}

            _boardGameNightRepository.UnattendBoardGameNight(user.Id, boardGameNight.Id);

            return Ok(new { Succes = true, Message = "You have unjoined the board game night!" });
        }


    }
}
