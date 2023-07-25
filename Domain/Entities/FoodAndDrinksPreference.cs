using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FoodAndDrinksPreference
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool LactoseFree { get; set; }

        [Required]
        public bool NutFree { get; set; }

        [Required]
        public bool AlcoholFree { get; set; }

        [Required]
        public bool Vegetarian { get; set; }

    }
}
