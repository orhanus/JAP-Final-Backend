using Core.Entities;
using Core.Interfaces.Repositories;
using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DataContext _context;

        public RatingRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<double> GetAverageRatingAsync(int mediaId)
        {
            return await _context.Ratings.Where(r => r.MediaId == mediaId).AverageAsync(r => r.Score);
        }
    }
}
