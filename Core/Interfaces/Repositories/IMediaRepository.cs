using Core.Entities;
using Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IMediaRepository
    {
        void UpdateMediaAsync(Media media);
        IQueryable<Media> GetMediaQuery();
        Task<bool> SaveAllAsync();
        Task<Media> GetMediaByIdAsync(int showId);
        Task<bool> AddMediaAsync(Media media);
        
    }
}
