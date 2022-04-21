using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Имейл")]
        public string Email { get; set; } = default!;

        [Display(Name = "Парола")]
        public string Password { get; set; } = default!;
    }
}
