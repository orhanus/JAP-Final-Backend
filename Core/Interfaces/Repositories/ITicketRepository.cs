using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<bool> BuyTicket(Ticket ticket);
        Task<int> GetNumberOfTicketsAlreadyBought(int screeningId, int userId);
    }
}
