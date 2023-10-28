using Domain.Entities;
using DomainServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly GameGatherDbContext _context;

        public ReviewRepository(GameGatherDbContext context)
        {
            _context = context;
        }

        public Review CreateReview(Review review)
        {
            // Save the review to the database
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

        public IEnumerable<Review> GetReviewsForBoardGameNightsHostedByUser(int userId)
        {
            // Get all board game nights hosted by the user
            var boardGameNights = _context.BoardGameNights
                .Include(b => b.Host)
                .Where(b => b.Host!.Id == userId)
                .ToList();

            // Get all reviews for the board game nights
            var reviews = new List<Review>();

            foreach (var boardGameNight in boardGameNights)
            {
                var reviewsForBoardGameNight = _context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.BoardGameNight)
                    .Where(r => r.BoardGameNight!.Id == boardGameNight.Id)
                    .ToList();

                reviews.AddRange(reviewsForBoardGameNight);
            }

            return reviews;
        }
    }
}
