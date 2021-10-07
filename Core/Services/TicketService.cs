using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IScreeningRepository _screeningRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketService(IAccountRepository accountRepository, IScreeningRepository screeningRepository,
            ITicketRepository ticketRepository)
        {
            _accountRepository = accountRepository;
            _screeningRepository = screeningRepository;
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Checks to see if screening exists, then checks to see if user exists
        /// If both exist, checks if screening is in the past, and if so throws an exception
        /// If user had already bought 10 tickets for the same screening he is unable to buy another
        /// </summary>
        /// <param name="ticketDto"></param>
        /// <returns></returns>
        public async Task<bool> BuyTicket(BuyTicketDto ticketDto)
        {
            var screening = await _screeningRepository.GetScreeningByIdAsync(ticketDto.ScreeningId);

            if (screening == null)
                throw new ArgumentException("Screening with given Id does not exist");

            var user = await _accountRepository.GetUserByIdAsync(ticketDto.UserId);

            if (user == null)
                throw new ArgumentException("User with given id does not exist");

            if (await _ticketRepository.GetNumberOfTicketsAlreadyBought(ticketDto.ScreeningId, ticketDto.UserId) 
                + ticketDto.NumberOfTickets > 10)
                throw new ArgumentException("Unable to buy more than 10 ticekts");



            for (int i = 0; i < ticketDto.NumberOfTickets; i++)
            {
                var ticket = new Ticket
                {
                    User = user,
                    Screening = screening,
                    Price = screening.TicketPrice,
                    Discount = 0d,
                };

                if (!await _ticketRepository.BuyTicket(ticket))
                    return false;
            }


            return true;
        }
    }
}
