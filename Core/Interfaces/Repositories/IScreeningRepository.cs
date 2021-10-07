using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IScreeningRepository
    {
        Task<ICollection<Screening>> GetScreeningsAsync();
        Task<Screening> GetScreeningByIdAsync(int id);
        Task<Screening> GetScreeningByMediaIdAsync(int mediaId);
        Task<bool> AddScreeningAsync(Screening screening);
        Task<bool> SaveAllAsync();
        void UpdateScreening(Screening screening);
    }
}
