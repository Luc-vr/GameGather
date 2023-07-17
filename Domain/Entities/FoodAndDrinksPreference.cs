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

        [Required(ErrorMessage = "Please specify lactose free food")]
        [Display(Name = "Lactose free:")]
        public bool LactoseFree { get; set; }

        [Required(ErrorMessage = "Please specify nut free food")]
        [Display(Name = "Nut free:")]
        public bool NutFree { get; set; }

        [Required(ErrorMessage = "Please specify alcohol free drinks")]
        [Display(Name = "Alcohol free:")]
        public bool AlcoholFree { get; set; }

        [Required(ErrorMessage = "Please specify vegetarian food")]
        [Display(Name = "Vegetarian:")]
        public bool Vegetarian { get; set; }

    }
}
