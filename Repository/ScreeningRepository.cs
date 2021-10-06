using Core.Entities;
using Core.Interfaces.Repositories;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ScreeningRepository : IScreeningRepository
    {
        private readonly DataContext _context;

        public ScreeningRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Screening> GetScreeningByIdAsync(int id)
        {
            return await _context.Screenings.FindAsync(id);
        }

        public async Task<Screening> GetScreeningByMediaIdAsync(int mediaId)
        {
            return await _context.Screenings.FirstOrDefaultAsync(screening => screening.MediaId == mediaId);
        }

        public async Task<ICollection<Screening>> GetScreeningsAsync()
        {
            return await _context.Screenings.Include(x => x.Movie).Include(x => x.Spectators).ToListAsync();
        }

        public void UpdateScreening(Screening screening)
        {
            _context.Entry(screening).State = EntityState.Modified;
        }

        public async Task<bool> AddScreeningAsync(Screening screening)
        {
            _context.Screenings.Add(screening);
            return await SaveAllAsync();
        }

        public async Task<int> GetNumberOfAlreadyReservedTickets(int userId, int screeningId)
        {
            var screening = await _context.Screenings.FindAsync(screeningId);
            return screening.Spectators.Count(x => x.Id == userId);
        }
    }
}
