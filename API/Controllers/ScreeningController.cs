using Core.Interfaces.Services;
using Core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost] 
        public async Task<ActionResult> Add(AddScreeningDto screeningDto)
        {
            if (await _screeningService.AddScreeningAsync(screeningDto))
                return NoContent();

            return StatusCode(500);
        }

        [HttpGet("{mediaId}")]
        public async Task<ActionResult<ICollection<ScreeningDto>>> GetScreeningByMediaId(int mediaId)
        {
            return Ok(await _screeningService.GetScreeningByMediaIdAsync(mediaId));
        }
    }
}
