using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IEmailService
    {
        Task<bool> SendUserCreatedEmail(string userName, string userEmail, string url);
    }
}
