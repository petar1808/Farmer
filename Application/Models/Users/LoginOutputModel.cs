using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class LoginOutputModel
    {
        public LoginOutputModel(string token, string role)
        {
            Token = token;
            Role = role;
        }

        public string Token { get; set; } = default!;

        public string Role { get; set; } = default!;
    }
}
