using Core.Models.DTOs;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IScreeningService
    {
        Task<bool> AddScreeningAsync(AddScreeningDto screeningDto);
        Task<ScreeningDto> GetScreeningByMediaIdAsync(int mediaId);
    }
}
