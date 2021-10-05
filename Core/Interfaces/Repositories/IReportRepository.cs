using Core.Entities.Procedures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IReportRepository
    {
        Task<List<MostWatched>> GetMostWatchedAsync();
        Task<List<MostScreened>> GetMostScreenedAsync(DateTime startDate, DateTime endDate);
        Task<List<MostRated>> GetMostRatedAsync();
    }
}
