using Domain.Entities;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public BoardGame GetBoardGameById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
