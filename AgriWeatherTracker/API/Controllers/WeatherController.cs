using AgriWeatherTracker.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        _weatherRepository = weatherRepository;
        _locationRepository = locationRepository;
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
        var location = await _locationRepository.GetByIdAsync(weatherDTO.LocationId);
        var weather = _mapper.Map<Weather>(weatherDTO);

        if (location != null) {
            _weatherRepository.SetEntityStateUnchanged(location);
            weather.Location = location;
        }

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

    // [HttpGet("calculate-health")]
    // public async Task<IActionResult> CalculateHealth([FromQuery] HealthRequestDto request)
    // {
    //     var weatherData = await _weatherService.GetWeatherData(request.CropId, request.LocationId, request.StartDate, request.EndDate);
    //     var healthScores = new List<HealthScoreDto>();

    //     foreach (var weather in weatherData)
    //     {
    //         var score = _evaluationService.Evaluate(weather);
    //         healthScores.Add(new HealthScoreDto { Date = weather.Date, Score = score });
    //     }

    //     return Ok(healthScores);
    // }

    [HttpDelete("byLocation/{locationId}")]
    public async Task<IActionResult> DeleteWeatherByLocation(int locationId)
    {
        // Check if the location actually exists
        var location = await _locationRepository.GetByIdAsync(locationId);
        if (location == null)
        {
            return NotFound($"Location with ID {locationId} not found.");
        }

        // Delete all weather data for the given location
        await _weatherRepository.DeleteWeatherByLocationIdAsync(locationId);
        return NoContent(); // Return a 204 No Content response indicating successful deletion
    }

    [HttpGet("byLocation/{locationId}/{days}")]
    public async Task<ActionResult<IEnumerable<Weather>>> GetWeatherByLocationAndDays(int locationId, int days)
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
}
