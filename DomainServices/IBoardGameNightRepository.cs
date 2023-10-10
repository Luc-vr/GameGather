using Domain.Entities;

namespace DomainServices
{
    public interface IBoardGameNightRepository
    {
        // Get board game night by id (US_01)
        BoardGameNight? GetBoardGameNightById(int id);

        // Get all upcoming hosting board game nights for a user
        ICollection<BoardGameNight> GetAllUpcomingHostingBoardGameNightsForUser(int userId);

        // Get all past hosting board game nights for a user
        ICollection<BoardGameNight> GetAllPastHostingBoardGameNightsForUser(int userId);

        // Get all attending board game nights for a user (US_01)
        ICollection<BoardGameNight> GetAllAttendingBoardGameNightsForUser(int userId);

        // Get all joinable board game nights for a user
        ICollection<BoardGameNight> GetAllJoinableBoardGameNightsForUser(int userId);

        // Create board game night (US_02)
        void CreateBoardGameNight(BoardGameNight boardGameNight);

        // Update board game night (US_02)
        void UpdateBoardGameNight(BoardGameNight boardGameNight);

        // Delete board game night (US_02)
        void DeleteBoardGameNight(BoardGameNight boardGameNight);

        // Attend board game night (US_04)
        void AttendBoardGameNight(int userId, int boardGameNightId);

        // Unattend board game night (US_04)
        void UnattendBoardGameNight(int userId, int boardGameNightId);

        // Get all reviews for a board game night (US_08)
        ICollection<Review> GetAllReviewsForBoardGameNight(int boardGameNightId);

        void AddBoardGameToBoardGameNight(int boardGameNightId, int boardGameId);

        void RemoveBoardGameFromBoardGameNight(int boardGameNightId, int boardGameId);

        ICollection<BoardGameNight> GetBoardGameNightsWithoutUserReview(int userId);
    }
}