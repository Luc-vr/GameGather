using Domain.Entities;

namespace DomainServices
{
    public interface IFoodAndDrinksPrefRepository
    {
        // Update food and drinks preferences
        void UpdateFoodAndDrinksPref(FoodAndDrinksPreference foodAndDrinksPref);
    }
}
