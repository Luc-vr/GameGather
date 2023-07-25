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

        public void AttendBoardGameNight(int boardGameNightId)
        {
            throw new NotImplementedException();
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

        public ICollection<BoardGameNight> GetAllAttendingBoardGameNightsForUser()
        {
            throw new NotImplementedException();
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

        public ICollection<Review> GetAllReviewsForBoardGameNight(int boardGameNightId)
        {
            throw new NotImplementedException();
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

        public void UnattendBoardGameNight(int boardGameNightId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBoardGameNight(BoardGameNight boardGameNight)
        {
            // Update the board game night in the database
            _context.BoardGameNights.Update(boardGameNight);
            _context.SaveChanges();
        }
    }
}
