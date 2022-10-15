using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class LoginOutputModel
    {
        public LoginOutputModel(string token)
        {
            Token = token;
        }

        public string Token { get; set; } = default!;
    }
}
