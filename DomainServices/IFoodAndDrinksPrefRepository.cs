using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IFoodAndDrinksPrefRepository
    {
        // Update food and drinks preferences
        FoodAndDrinksPreference UpdateFoodAndDrinksPref(FoodAndDrinksPreference foodAndDrinksPref);
    }
}
