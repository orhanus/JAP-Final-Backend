using Core.Models.DTOs;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ITicketService
    {
        Task<bool> BuyTicket(BuyTicketDto buyTicketDto);
    }
}
