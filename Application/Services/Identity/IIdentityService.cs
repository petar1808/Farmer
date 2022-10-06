using Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public interface IIdentityService
    {
        Task CreateUser(CreateUserModel createUserModel);
    }
}
