using DIY_API.DTOs.Challenge;
using DIY_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallenge _appService;

        public ChallengeController(IChallenge appService)
        {
            _appService = appService;

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllChallenges()
        {
            try
            {
                var response = await _appService.GetAllChallenges();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetChallengesById(int challangeId)
        {
            try
            {
                var response = await _appService.GetChallengesById(challangeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetChallengesByCategoryId(int categoryId)
        {
            try
            {
                var response = await _appService.GetChallengesByCategoryId(categoryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTopRatedChallenges()
        {
            try
            {
                var response = await _appService.GetTopRatedChallenges();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTopRecommendedChallenges()
        {
            try
            {
                var response = await _appService.GetTopRecommendedChallenges();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateChallenge([FromBody] CreateChallengeDTO input)
        {
            try
            {
                var response = await _appService.CreateChallenge(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateChallenges(int challangeId, [FromBody] UpdateChallengesDTO input)
        {
            try
            {
                var response = await _appService.UpdateChallenges(challangeId, input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> ChallengeActivationStatus(int challangeId, [FromBody] ChallengeActivationDTO input)
        {
            try
            {
                var response = await _appService.ChallengeActivationStatus(challangeId, input);
                if (response)
                {
                    return Ok("Challenge activation status updated successfully");
                }
                return NotFound("Challenge not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteChallenges(int challangeId)
        {
            try
            {
                var response = await _appService.DeleteChallenges(challangeId);
                if (response)
                {
                    return Ok("Challenge deleted successfully");
                }
                return NotFound("Challenge not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
