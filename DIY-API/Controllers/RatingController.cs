﻿using DIY_API.DTOs.Category;
using DIY_API.DTOs.Rating;
using DIY_API.Helper;
using DIY_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatings _appService;

        public RatingController(IRatings appService)
        {
            _appService = appService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRate([FromQuery] PaginationParameters pagination)
        {
            try
            {
                var response = await _appService.GetAllRate(pagination);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRateByCallaengeID(int id)
        {
            try
            {
                var response = await _appService.GetRateByCallaengeID(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRateByUserID(int id)
        {
            try
            {
                var response = await _appService.GetRateByUserID(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRate(CreateRateDTO input)
        {
            try
            {
                var response = await _appService.CreateRate(input);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> RateActivationStatus(int RateId,[FromBody] RateActivationDTO input)
        {
            try
            {
                var response = await _appService.RateActivationStatus(RateId, input);
                if (response)
                {
                    return Ok("Rate activation status updated successfully");
                }
                return NotFound("Rate not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteRate(int Id)
        {
            try
            {
                var response = await _appService.DeleteRate(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
