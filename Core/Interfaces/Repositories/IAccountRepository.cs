using Core.Entities;
using Core.Models.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<SignInResult> CheckPasswordSignInAsync(User user, string password);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
