using AgriWeatherTracker.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IMapper _mapper;

    public WeatherController(IWeatherRepository weatherRepository, IMapper mapper)
    {
        _weatherRepository = weatherRepository;
        _mapper = mapper;
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
    public async Task<ActionResult<Weather>> PostWeather(WeatherDTO weatherDTO)
    {
        var weather = _mapper.Map<Weather>(weatherDTO);
        await _weatherRepository.CreateWeatherAsync(weather);
        return CreatedAtAction("GetWeather", new { id = weather.Id }, weather);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWeather(int id, WeatherDTO weatherDTO)
    {
        if (id != weatherDTO.Id)
        {
            return BadRequest();
        }

        var weather = _mapper.Map<Weather>(weatherDTO);
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
