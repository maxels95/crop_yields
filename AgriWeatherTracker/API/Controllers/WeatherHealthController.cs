using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using AgriWeatherTracker.Models;
using AutoMapper;



[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class WeatherHealthController : ControllerBase
{
    private readonly WeatherHealthService _weatherHealthService;

    public WeatherHealthController(WeatherHealthService weatherHealthService)
    {
        _weatherHealthService = weatherHealthService;
    }

    [HttpPost("update-health-scores")]
    public async Task<IActionResult> UpdateHealthScores(int cropId, string startDate, string endDate)
    {
        DateTime startDt = DateTime.Parse(startDate);
        DateTime endDt = DateTime.Parse(endDate);
        var signals = await _weatherHealthService.UpdateHealthScoresForCrop(cropId, startDt, endDt);
        return Ok(signals);
    }
}
