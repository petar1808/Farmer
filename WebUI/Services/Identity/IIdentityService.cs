using WebUI.ServicesModel.Identity;

namespace WebUI.Services.Identity
{
    public interface IIdentityService
    {
        Task<List<ListUserModel>> ListUser();

        Task<string> Login(string email, string password);

        Task<bool> CreateUser(CreateUserModel createUserModel);

        Task<bool> CreateUserPassword(CreateUserPasswordModel createUserPasswordModel);
    }
}
