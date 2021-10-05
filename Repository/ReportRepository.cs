using Core.Entities.Procedures;
using Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces.Repositories;

namespace Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MostRated>> GetMostRatedAsync()
        {
            return await _context.TopMostRatings
                .FromSqlRaw("EXEC [dbo].[spGetTopMostRatings]")
                .ToListAsync();
        }

        public async Task<List<MostScreened>> GetMostScreenedAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TopMostScreenings
                .FromSqlRaw("EXEC [dbo].[spGetTopWithMostScreenings] {0}, {1}", startDate, endDate)
                .ToListAsync();
        }

        public async Task<List<MostWatched>> GetMostWatchedAsync()
        {
            return await _context.TopMostSoldTickets
                .FromSqlRaw("EXEC [dbo].[spGetTopWithMostSoldTicketsWithoutRating]")
                .ToListAsync();
        }
    }
}
