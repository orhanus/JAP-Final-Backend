using Core.Models.DTOs;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IRatingService
    {
        Task<double> GetAverageRatingAsync(int mediaId);
        Task<bool> AddRatingAsync(int mediaId, RatingDto rating);
    }
}
