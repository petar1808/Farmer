using Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<List<ListUserModel>> ListUsers();

        Task<GetUserModel> GetUser(string id);

        Task EditUser(string id, bool active);

    }
}
