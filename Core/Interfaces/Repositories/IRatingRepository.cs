using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IRatingRepository
    {
        Task<bool> AddRatingAsync(Rating rating);
        Task<double> GetAverageRatingAsync(int mediaId);
    }
}
