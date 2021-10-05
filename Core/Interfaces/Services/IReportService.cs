using Core.Entities.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IReportService
    {
        Task<List<MostRated>> GetMostRatedAsync();
        Task<List<MostScreened>> GetMostScreenedAsync(DateTime startDate, DateTime endDate);
        Task<List<MostWatched>> GetMostWatchedAsync();
    }
}
