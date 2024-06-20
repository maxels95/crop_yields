using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using AgriWeatherTracker.Models;
using AutoMapper;
using System;
using System.Globalization;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class WeatherHealthController : ControllerBase
{
    private readonly WeatherHealthService _weatherHealthService;

    public WeatherHealthController(WeatherHealthService weatherHealthService)
    {
        _weatherHealthService = weatherHealthService ?? throw new ArgumentNullException(nameof(weatherHealthService));
    }

    [HttpPost("update-health-scores")]
    public async Task<IActionResult> UpdateHealthScores(int cropId, string startDate, string endDate)
    {
        try
        {
            if (!DateTime.TryParse(startDate, out DateTime startDt))
            {
                return BadRequest("Invalid start date format. Please use a valid date format (e.g., 'YYYY-MM-DD').");
            }

            if (!DateTime.TryParse(endDate, out DateTime endDt))
            {
                return BadRequest("Invalid end date format. Please use a valid date format (e.g., 'YYYY-MM-DD').");
            }

            if (startDt > endDt)
            {
                return BadRequest("Start date must be earlier than end date.");
            }

            var signals = await _weatherHealthService.UpdateHealthScoresForCrop(cropId, startDt, endDt);
            return Ok(signals);
        }
        catch (Exception ex)
        {
            // Log the exception details here to troubleshoot the issue
            return StatusCode(500, $"An internal error occurred: {ex.Message}");
        }
    }
}
