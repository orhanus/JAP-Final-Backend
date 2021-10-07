using Core.Interfaces.Services;
using Core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class TicketController : BaseApiController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Authorize(Policy = "RequireCustomerRole")]
        [HttpPost("{mediaId}/buy")]
        public async Task<ActionResult> Buy(BuyTicketDto ticketDto)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _ticketService.BuyTicket(ticketDto))
                return Ok();

            return BadRequest("Failed to buy ticket(s) for screening");
        }
    }
}
