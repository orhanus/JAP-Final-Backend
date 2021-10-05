using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ScreeningService : IScreeningService
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IAccountRepository _accountRepository;

        public ScreeningService(IScreeningRepository screeningRepository, IAccountRepository accountRepository)
        {
            _screeningRepository = screeningRepository;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Checks to see if screening exists, then checks to see if user exists
        /// If both exist, adds user to Spectators of the screening
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> AddUserToScreeningAsync(int mediaId, string username)
        {
            var screening = await _screeningRepository.GetScreeningByMediaIdAsync(mediaId);

            if (screening == null)
                throw new ArgumentException("Screening with given mediaId does not exist");

            var user = await _accountRepository.GetUserByUsernameAsync(username);

            if (user == null)
                throw new ArgumentException("User with given username does not exist");

            screening.Spectators.Add(user);

            _screeningRepository.UpdateScreening(screening);

            return await _screeningRepository.SaveAllAsync();
        }
    }
}
