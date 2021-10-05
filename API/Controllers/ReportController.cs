using Core.Entities.Procedures;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ReportController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("most-rated")]
        public async Task<ActionResult<MostRated>> GetMostRated()
        {
            return Ok(await _reportService.GetMostRatedAsync());
        }

        [HttpGet("most-watched")]
        public async Task<ActionResult<MostWatched>> GetMostWatched()
        {
            return Ok(await _reportService.GetMostWatchedAsync());
        }

        [HttpGet("most-screened")]
        public async Task<ActionResult<MostScreened>> GetMostScreened(DateTime startDate, DateTime endDate)
        {
            return Ok(await _reportService.GetMostScreenedAsync(startDate, endDate));
        }
    }
}
