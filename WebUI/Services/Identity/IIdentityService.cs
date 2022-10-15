using WebUI.ServicesModel.Identity;

namespace WebUI.Services.Identity
{
    public interface IIdentityService
    {
        Task<List<ListUserModel>> ListUser();
    }
}
