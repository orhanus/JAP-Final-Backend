using Core.Entities;
using Core.Models.Models;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task RegisterUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
