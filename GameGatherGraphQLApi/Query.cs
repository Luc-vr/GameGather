using Domain.Entities;
using Infrastructure;

namespace GameGatherGraphQLApi
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BoardGame> GetBoardGames([Service] GameGatherDbContext context) =>
            context.BoardGames;

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BoardGameNight> GetBoardGameNights([Service] GameGatherDbContext context) =>
            context.BoardGameNights;
    }
}
