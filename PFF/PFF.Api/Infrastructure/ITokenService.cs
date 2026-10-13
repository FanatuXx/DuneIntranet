using PFF.Domain.Model.Entities;

namespace PFF.Api.Infrastructure
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
        int GetUserIdFromToken(string token);
    }
}
