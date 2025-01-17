﻿using HotChocolate;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class FoodAndDrinksPreference
    {
        [Key]
        [GraphQLIgnore]
        public int Id { get; set; }

        [Required]
        public bool LactoseFree { get; set; }

        [Required]
        public bool NutFree { get; set; }

        [Required]
        public bool AlcoholFree { get; set; }

        [Required]
        public bool Vegetarian { get; set; }

        [GraphQLIgnore]
        public bool IsCompatibleWith(FoodAndDrinksPreference otherPreference)
        {
            // Check compatibility for each property
            bool lactoseCompatible = LactoseFree || !otherPreference.LactoseFree;
            bool nutCompatible = NutFree || !otherPreference.NutFree;
            bool alcoholCompatible = AlcoholFree || !otherPreference.AlcoholFree;
            bool vegetarianCompatible = Vegetarian || !otherPreference.Vegetarian;

            // Check if all properties are compatible
            return lactoseCompatible && nutCompatible && alcoholCompatible && vegetarianCompatible;
        }

    }
}
