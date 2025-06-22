using DIY_API.Helper;
using DIY_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeResultsController : ControllerBase
    {
        private readonly IChallengeResult _appService;

        public ChallengeResultsController(IChallengeResult appService)
        {
            _appService = appService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllChallengeResult()
        {
            try
            {
                var response = await _appService.GetAllChallengeResult();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetChallengeResultByChallengeId(int challengeId)
        {
            try
            {
                var response = await _appService.GetChallengeResultByChallengeId(challengeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetChallengeResultByUesrId(int UserId)
        {
            try
            {
                var response = await _appService.GetChallengeResultByUesrId(UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
