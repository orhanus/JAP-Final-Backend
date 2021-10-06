using Core.Models.DTOs;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IScreeningService
    {
        Task<bool> AddUserToScreeningAsync(int mediaId, string username);
        Task<bool> AddScreeningAsync(AddScreeningDto screeningDto);
    }
}
