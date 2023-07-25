using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EventPreferencesViewModel
    {
        [Required(ErrorMessage = "Please specify if you will offer lactose free food/drinks")]
        [Display(Name = "I will offer lactose free food/drinks:")]
        public bool LactoseFree { get; set; } = false;

        [Required(ErrorMessage = "Please specify if you will offer nut free food")]
        [Display(Name = "I will offer nut free food:")]
        public bool NutFree { get; set; } = false;

        [Required(ErrorMessage = "Please specify if you will offer alcohol free drinks")]
        [Display(Name = "I will offer alcohol free drinks:")]
        public bool AlcoholFree { get; set; } = false;

        [Required(ErrorMessage = "Please specify if you will offer vegatarian food/drinks")]
        [Display(Name = "I will offer vegatarian food/drinks:")]
        public bool Vegetarian { get; set; } = false;
    }
}
