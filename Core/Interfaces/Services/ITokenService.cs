using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}
