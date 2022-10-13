using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class ForgotPasswordModel
    {
        public string Email { get; init; } = default!;

        public string ChangePasswordUrl { get; init; } = default!;
    }
}
