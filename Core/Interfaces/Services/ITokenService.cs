using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}
