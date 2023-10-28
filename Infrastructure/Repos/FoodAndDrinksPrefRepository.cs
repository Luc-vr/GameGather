using Domain.Entities;
using DomainServices;

namespace Infrastructure.Repos
{
    public class FoodAndDrinksPrefRepository : IFoodAndDrinksPrefRepository
    {
        private readonly GameGatherDbContext _context;

        public FoodAndDrinksPrefRepository(GameGatherDbContext context)
        {
            _context = context;
        }

        public void UpdateFoodAndDrinksPref(FoodAndDrinksPreference foodAndDrinksPref)
        {
            _context.FoodAndDrinksPreferences.Update(foodAndDrinksPref);
            _context.SaveChanges();
        }
    }
}
