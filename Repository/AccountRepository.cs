using Core.Entities;
using Core.Interfaces.Repositories;
using Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == username);
        }
    }
}
