using Domain.Entities;
using DomainServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly GameGatherDbContext _context;

        public BoardGameRepository(GameGatherDbContext context)
        {
            _context = context;
        }

        public ICollection<BoardGame> GetAllBoardGames()
        {
            // Get all board games
            return _context.BoardGames.ToList();
        }

        public ICollection<BoardGame> GetAllBoardGamesNotInList(ICollection<BoardGame> boardGames)
        {
            // Get all board games that are not in the list
            return _context.BoardGames
                .Where(bg => !boardGames.Contains(bg))
                .ToList();
        }

        public BoardGame? GetBoardGameById(int id)
        {
            // Get board game by id
            return _context.BoardGames
                .Include(bg => bg.BoardGameNights)
                .FirstOrDefault(bg => bg.Id == id);
        }
    }
}
