
using Application.Models.Users;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public UserService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task EditUser(string id, bool active)
        {
            var user = await farmerDbContext
                .Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new ApplicationException($"Arable land with Id: {id}, don't exist");
            }

            user.UpdateActive(active);

            farmerDbContext.Update(user);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<GetUserModel> GetUser(string id)
        {
            var user = await farmerDbContext
                .Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new ApplicationException($"User with Id: {id}, don't exist");
            }

            var result = mapper.Map<GetUserModel>(user);
            return result;
        }

        public async Task<List<ListUserModel>> ListUsers()
        {
            var listUser = await farmerDbContext
                 .Users
                 .Select(x => new ListUserModel
                 {
                     Id = x.Id,
                     Email = x.UserName,
                     Active = x.Active,
                     Role = x.UserRoles.First().Name
                 })
                 .ToListAsync();

            return listUser;
        }
    }
}
