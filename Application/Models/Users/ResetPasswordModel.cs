using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class ResetPasswordModel
    {
        public string Email { get; init; } = default!;

        public string NewPassword { get; init; } = default!;

        public string Token { get; init; } = default!;
    }
}
