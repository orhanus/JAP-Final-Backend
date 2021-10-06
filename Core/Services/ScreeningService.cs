using AutoMapper;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ScreeningService : IScreeningService
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ScreeningService(IScreeningRepository screeningRepository,
            IAccountRepository accountRepository, IMapper mapper)
        {
            _screeningRepository = screeningRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddScreeningAsync(AddScreeningDto screeningDto)
        {
            Screening screening = _mapper.Map<Screening>(screeningDto);

            return await _screeningRepository.AddScreeningAsync(screening);
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
