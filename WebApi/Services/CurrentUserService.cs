using Application.Services;
using System.Security.Claims;

namespace WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public int UserTenantId
        {
            get
            {
                var tenantIdClaim = this._httpContextAccessor
                        .HttpContext?
                        .User?
                        .FindFirstValue("TenantId") ??
                    throw new Exception("Този потребител няма TenantId");

                if (int.TryParse(tenantIdClaim, out int tenantId))
                    return tenantId;
                else
                    throw new Exception("Този потребител има грешен TenantId");
            }
        }
    }
}
