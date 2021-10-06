using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Enums;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using Core.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MediaController : BaseApiController
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet("{mediaType}", Name = "GetShows")]
        public async Task<ActionResult<ICollection<MediaDto>>> GetShows(
            [FromQuery]MediaParams mediaParams, 
            MediaType mediaType) 
        {
            var media = await _mediaService.GetMediaAsync(mediaParams, mediaType);
            return Ok(media);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add")]
        public async Task<ActionResult> AddMedia(AddMediaDto addMediaDto)
        {
            if(await _mediaService.AddMediaAsync(addMediaDto))
                return CreatedAtRoute("GetShows", new { }, addMediaDto);

            return BadRequest("Unable to add media");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateMedia(UpdateMediaDto updateMediaDto)
        {
            if (await _mediaService.UpdateMediaAsync(updateMediaDto))
                return NoContent();

            return BadRequest("Unable to update media");
        }
    }
}