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
        public int UserTenantId => GetUserTenantId();

        private int GetUserTenantId()
        {
            var tenantIdClaim = _httpContextAccessor.HttpContext?
                .User?
                .FindFirstValue("TenantId");

            if (tenantIdClaim == null)
            {
                throw new InvalidOperationException("Този потребител няма TenantId");
            }

            if (int.TryParse(tenantIdClaim, out int tenantId))
            {
                return tenantId;
            }
            else
            {
                throw new FormatException("Този потребител има грешен TenantId");
            }
        }
    }
}
