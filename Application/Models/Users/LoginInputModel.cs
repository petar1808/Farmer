﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class LoginInputModel
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
