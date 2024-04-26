using AgriWeatherTracker.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherRepository _weatherRepository;

    public WeatherController(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Weather>>> GetAllWeather()
    {
        var weathers = await _weatherRepository.GetAllWeatherAsync();
        return Ok(weathers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Weather>> GetWeather(int id)
    {
        var weather = await _weatherRepository.GetWeatherByIdAsync(id);
        if (weather == null)
        {
            return NotFound();
        }
        return Ok(weather);
    }

    [HttpGet("byCropAndLocation")]
    public async Task<ActionResult<IEnumerable<Weather>>> GetWeatherByLocation(int locationId)
    {
        var weathers = await _weatherRepository.GetWeatherByLocationAsync(locationId);
        if (weathers == null || !weathers.Any())
        {
            return NotFound();
        }
        return Ok(weathers);
    }

    [HttpPost]
    public async Task<ActionResult<Weather>> PostWeather(Weather weather)
    {
        await _weatherRepository.CreateWeatherAsync(weather);
        return CreatedAtAction("GetWeather", new { id = weather.Id }, weather);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWeather(int id, Weather weather)
    {
        if (id != weather.Id)
        {
            return BadRequest();
        }

        await _weatherRepository.UpdateWeatherAsync(weather);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWeather(int id)
    {
        await _weatherRepository.DeleteWeatherAsync(id);
        return NoContent();
    }
}
