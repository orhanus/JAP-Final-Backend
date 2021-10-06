using Core.Interfaces.Services;
using Core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ScreeningController : BaseApiController
    {
        private readonly IScreeningService _screeningService;

        public ScreeningController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        [Authorize(Policy = "RequireCustomerRole")]
        [HttpPost("{mediaId}/ticket")]
        public async Task<ActionResult> ReserveTicket(int mediaId)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(await _screeningService.AddUserToScreeningAsync(mediaId, username))
                return Ok();

            return BadRequest("Failed to reserve ticket for screening");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost] 
        public async Task<ActionResult> Add(AddScreeningDto screeningDto)
        {
            if (await _screeningService.AddScreeningAsync(screeningDto))
                return NoContent();

            return StatusCode(500);
        }
    }
}
