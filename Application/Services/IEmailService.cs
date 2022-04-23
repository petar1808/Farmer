using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IEmailService
    {
        Task SendUserChangePassword(string userEmail, string password);

        Task SendUserCreatedEmail(string userEmail, string password);
    }
}
