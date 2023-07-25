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
    public class ReviewRepository : IReviewRepository
    {
        private readonly GameGatherDbContext _context;

        public ReviewRepository(GameGatherDbContext context)
        {
            _context = context;
        }

        public Review CreateReview(Review review, int boardGameNightId)
        {
            throw new NotImplementedException();
        }
    }
}
