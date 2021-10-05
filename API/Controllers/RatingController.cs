using Core.Interfaces.Services;
using Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("{mediaId}/rate")]
        public async Task<ActionResult> AddRating(int mediaId, RatingDto rating)
        {
            if (await _ratingService.AddRatingAsync(mediaId, rating))
            {
                return Ok();
            }
            return BadRequest("Unable to add rating");
        }
    }
}
