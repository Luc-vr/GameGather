using Domain.Entities;

namespace DomainServices
{
    public interface IBoardGameRepository
    {
        // Get all board games
        ICollection<BoardGame> GetAllBoardGames();

        // Get board game by id
        BoardGame? GetBoardGameById(int id);


        ICollection<BoardGame> GetAllBoardGamesNotInList(ICollection<BoardGame> boardGames);
    }
}