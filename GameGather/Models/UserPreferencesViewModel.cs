using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class UserPreferencesViewModel
    {
        [Required(ErrorMessage = "Please specify if you want lactose free food/drinks")]
        [Display(Name = "I would like lactose free food/drinks:")]
        public bool LactoseFree { get; set; } = false;

        [Required(ErrorMessage = "Please specify if you want nut free food")]
        [Display(Name = "I would like nut free food:")]
        public bool NutFree { get; set; } = false;

        [Required(ErrorMessage = "Please specify if you want alcohol free drinks")]
        [Display(Name = "I would like alcohol free drinks:")]
        public bool AlcoholFree { get; set; } = false;

        [Required(ErrorMessage = "Please specify if you want vegatarian food/drinks")]
        [Display(Name = "I would like vegatarian food/drinks:")]
        public bool Vegetarian { get; set; } = false;
    }
}
