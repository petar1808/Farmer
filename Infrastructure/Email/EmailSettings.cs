using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class EmailSettings
    {
        public string Server { get; set; } = default!;

        public int Port { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string Sender { get; set; } = default!;

        public string ServerName { get; set; } = default!;
    }
}
