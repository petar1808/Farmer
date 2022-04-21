using Application.Mappings;
using Application.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.ViewModels
{
    public class EditUserViewModel : IMapFrom<GetUserModel>
    {
        public string Id { get; set; } = default!;

        [Display(Name = "Имейл")]
        public string Email { get; init; } = default!;

        [Display(Name = "Активен/Неактивен")]
        public bool Active { get; init; } = default!;
    }
}
