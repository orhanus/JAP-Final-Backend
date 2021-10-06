using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Checks if the user exists and if it does matches found user's password hash with a calculated password hash.
        /// If those two hashes are matching, returns a UserDto object of the user.
        /// If not throws an ArgumentException
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username);

            if (user == null)
                throw new UnauthorizedAccessException("Username is invalid");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                throw new ArgumentException("Password is invalid");

            return new UserDto
            {
                Username = loginDto.Username,
                Token = await _tokenService.CreateTokenAsync(user)
            };
        }

        /// <summary>
        /// Checks if a user with the same username exists, if so throws an ArgumentException
        /// If username is not taken, creates a new User object and assigns properties to it before saving the new User to the database
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        public async Task RegisterAsync(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
                throw new ArgumentException("Username is taken");

            var user = new User
            {
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new ArgumentException(result.Errors.ToString());

            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

            if (!roleResult.Succeeded)
                throw new ArgumentException(roleResult.Errors.ToString());
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username);
        }
    }
}
