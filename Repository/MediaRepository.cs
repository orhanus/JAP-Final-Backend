using Core.Entities;
using Core.Interfaces.Repositories;
using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class MediaRepository : IMediaRepository
    {
        private readonly DataContext _context;

        public MediaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMediaAsync(Media media)
        {
            _context.Media.Add(media);

            return await SaveAllAsync();
        }

        public async Task<Media> GetMediaByIdAsync(int showId)
        {
            return await _context.Media.FindAsync(showId);
        }

        public IQueryable<Media> GetMediaQuery()
        {
            return _context.Media.Include(x => x.Actors).Include(x => x.Screenings);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateMedia(Media media)
        {
            _context.Entry(media).State = EntityState.Modified;
        }
    }
}
