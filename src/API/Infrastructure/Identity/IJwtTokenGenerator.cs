using Application.Models;

namespace Infrastructure.Identity
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, string role);
    }
}
