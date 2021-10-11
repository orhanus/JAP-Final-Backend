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

        public async Task<ScreeningDto> GetScreeningByMediaIdAsync(int mediaId)
        {
            return _mapper.Map<ScreeningDto>(await _screeningRepository.GetScreeningByMediaIdAsync(mediaId));
        }
    }
}
