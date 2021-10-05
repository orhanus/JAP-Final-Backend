using Core.Entities.Procedures;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<List<MostRated>> GetMostRatedAsync()
        {
            return await _reportRepository.GetMostRatedAsync();
        }

        public async Task<List<MostScreened>> GetMostScreenedAsync(DateTime startDate, DateTime endDate)
        {
            return await _reportRepository.GetMostScreenedAsync(startDate, endDate);
        }

        public async Task<List<MostWatched>> GetMostWatchedAsync()
        {
            return await _reportRepository.GetMostWatchedAsync();
        }
    }
}
