using Core.Entities;
using Core.Interfaces.Repositories;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> BuyTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetNumberOfTicketsAlreadyBought(int screeningId, int userId)
        {
            return await _context.Tickets
                .CountAsync(t => t.ScreeningId == screeningId && t.UserId == userId);
        }
    }
}
