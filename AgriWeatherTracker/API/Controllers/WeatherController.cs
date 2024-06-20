using AgriWeatherTracker.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public WeatherController(IWeatherRepository weatherRepository, IMapper mapper,
                    ILocationRepository locationRepository)
    {
        _weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
        _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Weather>>> GetAllWeather()
    {
        try
        {
            var weathers = await _weatherRepository.GetAllWeatherAsync();
            return Ok(weathers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving all weather data.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Weather>> GetWeather(int id)
    {
        try
        {
            var weather = await _weatherRepository.GetWeatherByIdAsync(id);
            if (weather == null)
            {
                return NotFound();
            }
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving weather data for ID {id}.");
        }
    }

    [HttpGet("byCropAndLocation")]
    public async Task<ActionResult<IEnumerable<Weather>>> GetWeatherByLocation(int locationId)
    {
        try
        {
            var weathers = await _weatherRepository.GetWeatherByLocationAsync(locationId);
            if (weathers == null || !weathers.Any())
            {
                return NotFound();
            }
            return Ok(weathers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving weather data for location ID {locationId}.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Weather>> PostWeather(WeatherDTO weatherDTO)
    {
        try
        {
            var location = await _locationRepository.GetByIdAsync(weatherDTO.LocationId);
            if (location == null)
            {
                return NotFound($"Location with ID {weatherDTO.LocationId} not found.");
            }

            var weather = _mapper.Map<Weather>(weatherDTO);
            _weatherRepository.SetEntityStateUnchanged(location);
            weather.Location = location;

            await _weatherRepository.CreateWeatherAsync(weather);
            return CreatedAtAction("GetWeather", new { id = weather.Id }, weather);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while posting the weather data.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWeather(int id, WeatherDTO weatherDTO)
    {
        try
        {
            if (id != weatherDTO.Id)
            {
                return BadRequest("Mismatched ID in request.");
            }

            var weather = await _weatherRepository.GetWeatherByIdAsync(id);
            if (weather == null)
            {
                return NotFound();
            }

            _mapper.Map(weatherDTO, weather);
            await _weatherRepository.UpdateWeatherAsync(weather);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating weather data for ID {id}.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWeather(int id)
    {
        try
        {
            var weather = await _weatherRepository.GetWeatherByIdAsync(id);
            if (weather == null)
            {
                return NotFound();
            }

            await _weatherRepository.DeleteWeatherAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting weather data for ID {id}.");
        }
    }

    [HttpDelete("byLocation/{locationId}")]
    public async Task<IActionResult> DeleteWeatherByLocation(int locationId)
    {
        try
        {
            var location = await _locationRepository.GetByIdAsync(locationId);
            if (location == null)
            {
                return NotFound($"Location with ID {locationId} not found.");
            }

            await _weatherRepository.DeleteWeatherByLocationIdAsync(locationId);
            return NoContent(); // Return a 204 No Content response indicating successful deletion
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting weather data for location ID {locationId}.");
        }
    }

    [HttpGet("byLocation/{locationId}/{days}")]
    public async Task<ActionResult<IEnumerable<Weather>>> GetWeatherByLocationAndDays(int locationId, int days)
    {
        try
        {
            DateTime endDate = DateTime.UtcNow;
            DateTime startDate = endDate.AddDays(-days);  // Calculate the start date by subtracting days from today

            var weathers = await _weatherRepository.GetWeatherByLocationAndDateRangeAsync(locationId, startDate, endDate);
            if (weathers == null || !weathers.Any())
            {
                return NotFound($"No weather data found for location ID {locationId} from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.");
            }
            return Ok(weathers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving weather data for location ID {locationId} over the last {days} days.");
        }
    }
}
