using Infrastructure.Enum;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.ViewModels
{
    public class AddUserViewModel
    {
        [Display(Name = "Имейл")]
        [Required]
        public string Email { get; init; } = default!;

        [Display(Name = "Парола")]
        [Required]
        public string Password { get; init; } = default!;

        [Required]
        [Display(Name = "Роля")]
        public UserRoles Role { get; init; } = default!;
    }
}
