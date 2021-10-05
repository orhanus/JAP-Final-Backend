using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
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
            User user = await _accountRepository.GetUserByUsernameAsync(loginDto.Username);

            if (user == null)
                throw new UnauthorizedAccessException("Username is invalid");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != user.PasswordHash[i]) 
                    throw new UnauthorizedAccessException("Invalid password");

            return new UserDto
            {
                Username = loginDto.Username,
                Token = _tokenService.CreateToken(user)
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
            User existingUser = await _accountRepository.GetUserByUsernameAsync(registerDto.Username);

            if (existingUser != null)
                throw new ArgumentException("Username is taken");

            using var hmac = new HMACSHA512();

            User user = new User
            {
                Username = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _accountRepository.RegisterUserAsync(user);

        }
    }
}
