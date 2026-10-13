using PFF.Shared.Dtos;

namespace PFF.Domain.Repositories
{
    public interface IAuthRepository
    {
        string Authenticate(LoginDto loginDto);
    }
}
