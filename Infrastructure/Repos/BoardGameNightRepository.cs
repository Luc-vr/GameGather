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
    public class BoardGameNightRepository : IBoardGameNightRepository
    {
        private readonly GameGatherDbContext _context;

        public BoardGameNightRepository(GameGatherDbContext context)
        {
            _context = context;
        }

        public void AttendBoardGameNight(int userId, int boardGameNightId)
        {
            // Add the user to the board game night's attendees
            var boardGameNight = _context.BoardGameNights
                .Include(bgn => bgn.Attendees)
                .FirstOrDefault(bgn => bgn.Id == boardGameNightId);

            if (boardGameNight == null)
            {
                throw new ArgumentException("Board game night not found");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            boardGameNight.Attendees!.Add(user);
            _context.SaveChanges();
        }

        public void CreateBoardGameNight(BoardGameNight boardGameNight)
        {
            // Add the board game night to the database
            _context.BoardGameNights.Add(boardGameNight);
            _context.SaveChanges();
        }

        public void DeleteBoardGameNight(BoardGameNight boardGameNight)
        {
            // Remove the board game night from the database
            _context.BoardGameNights.Remove(boardGameNight);
            _context.SaveChanges();
        }

        public ICollection<User> GetAllAttendeesForBoardGameNight(int boardGameNightId)
        {
            throw new NotImplementedException();
        }

        public ICollection<BoardGameNight> GetAllAttendingBoardGameNightsForUser(int userId)
        {
            // Get all board game nights where the user is an attendee
            return _context.BoardGameNights
                .Include(bgn => bgn.Host)
                .Include(bgn => bgn.BoardGames)
                .Where(bgn => bgn.Attendees!.Any(a => a.Id == userId))
                .ToList();
        }

        public ICollection<BoardGameNight> GetAllBoardGameNights()
        {
            throw new NotImplementedException();
        }

        public ICollection<BoardGame> GetAllBoardGamesForBoardGameNight(int boardGameNightId)
        {
            throw new NotImplementedException();
        }

        public ICollection<BoardGameNight> GetAllHostingBoardGameNightsForUser(int userId)
        {
            // Get all board game nights where the user is the host
            return _context.BoardGameNights
                .Include(bgn => bgn.Attendees)
                .Include(bgn => bgn.BoardGames)
                .Where(bgn => bgn.Host!.Id == userId)
                .ToList();
        }

        public ICollection<BoardGameNight> GetAllJoinableBoardGameNightsForUser(int userId)
        {
            // Get all board game nights where the user is not the host, the user has not already joined
            // and the date is in the future sorted by date
            return _context.BoardGameNights
                .Include(bgn => bgn.Host)
                .Include(bgn => bgn.BoardGames)
                .Where(bgn => bgn.Host!.Id != userId && !bgn.Attendees!.Any(a => a.Id == userId) && bgn.DateTime > DateTime.Now)
                .OrderBy(bgn => bgn.DateTime)
                .ToList();
        }

        public ICollection<BoardGameNight> GetAllPastHostingBoardGameNightsForUser(int userId)
        {
            // Get all board game nights where the user is the host and the date is in the past sorted by date where most recent is first
            return _context.BoardGameNights
                .Include(bgn => bgn.Attendees)
                .Include(bgn => bgn.BoardGames)
                .Where(bgn => bgn.Host!.Id == userId && bgn.DateTime < DateTime.Now)
                .OrderByDescending(bgn => bgn.DateTime)
                .ToList();

        }

        public ICollection<Review> GetAllReviewsForBoardGameNight(int boardGameNightId)
        {
            throw new NotImplementedException();
        }

        public ICollection<BoardGameNight> GetAllUpcomingHostingBoardGameNightsForUser(int userId)
        {
            // Get all board game nights where the user is the host and the date is in the future sorted by date
            return _context.BoardGameNights
                .Include(bgn => bgn.Attendees)
                .Include(bgn => bgn.BoardGames)
                .Where(bgn => bgn.Host!.Id == userId && bgn.DateTime > DateTime.Now)
                .OrderBy(bgn => bgn.DateTime)
                .ToList();
        }

        public BoardGameNight? GetBoardGameNightById(int id)
        {
            // Get the board game night with the given id
            return _context.BoardGameNights
                .Include(bgn => bgn.Attendees)
                .Include(bgn => bgn.BoardGames)
                .Include(bgn => bgn.Host)
                .Include(bgn => bgn.Reviews)
                .Include(bgn => bgn.FoodAndDrinksPreference)
                .FirstOrDefault(bgn => bgn.Id == id);
        }

        public FoodAndDrinksPreference GetFoodAndDrinksPreferenceForBoardGameNight(int boardGameNightId)
        {
            throw new NotImplementedException();
        }

        public void UnattendBoardGameNight(int userId, int boardGameNightId)
        {
            // Remove the user from the board game night's attendees
            var boardGameNight = _context.BoardGameNights
                .Include(bgn => bgn.Attendees)
                .FirstOrDefault(bgn => bgn.Id == boardGameNightId);
            if (boardGameNight == null)
            {
                throw new ArgumentException("Board game night not found");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            boardGameNight.Attendees!.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateBoardGameNight(BoardGameNight boardGameNight)
        {
            // Update the board game night in the database
            _context.BoardGameNights.Update(boardGameNight);
            _context.SaveChanges();
        }

        public void AddBoardGameToBoardGameNight(int boardGameNightId, int boardGameId)
        {
            // Add the board game to the board game night's board games
            var boardGameNight = _context.BoardGameNights
                .Include(bgn => bgn.BoardGames)
                .FirstOrDefault(bgn => bgn.Id == boardGameNightId);
            if (boardGameNight == null)
            {
                throw new ArgumentException("Board game night not found");
            }

            var boardGame = _context.BoardGames.Find(boardGameId);
            if (boardGame == null)
            {
                throw new ArgumentException("Board game not found");
            }

            boardGameNight.BoardGames!.Add(boardGame);
            _context.SaveChanges();
        }

        public void RemoveBoardGameFromBoardGameNight(int boardGameNightId, int boardGameId)
        {
            // Remove the board game from the board game night's board games
            var boardGameNight = _context.BoardGameNights
                .Include(bgn => bgn.BoardGames)
                .FirstOrDefault(bgn => bgn.Id == boardGameNightId);
            if (boardGameNight == null)
            {
                throw new ArgumentException("Board game night not found");
            }

            var boardGame = _context.BoardGames.Find(boardGameId);
            if (boardGame == null)
            {
                throw new ArgumentException("Board game not found");
            }

            boardGameNight.BoardGames!.Remove(boardGame);
            _context.SaveChanges();
        }

        public ICollection<BoardGameNight> GetBoardGameNightsWithoutUserReview(int userId)
        {
            // Get all board game night where the user has attended but not written a review for
            var boardGameNights = _context.BoardGameNights
                .Include(bgn => bgn.Attendees)
                .Include(bgn => bgn.Reviews)
                .Include(bgn => bgn.Host)
                .Where(
                    bgn => bgn.Attendees!.Any(a => a.Id == userId) &&
                    !bgn.Reviews!.Any(r => r.User!.Id == userId) &&
                    bgn.DateTime < DateTime.Now
                 )
                .ToList();

            return boardGameNights;
        }
    }
}
