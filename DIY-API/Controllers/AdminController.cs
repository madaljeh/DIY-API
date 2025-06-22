using DIY_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _appService;

        public AdminController(IAdmin appService)
        {
            _appService = appService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var statistics = await _appService.GetStatisticsAsync();
                if (statistics == null)
                {
                    return NotFound("Statistics not found.");
                }
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
