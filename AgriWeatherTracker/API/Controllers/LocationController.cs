using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationController(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    // GET: api/Location
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAllLocations()
    {
        try
        {
            var locations = await _locationRepository.GetAllAsync();
            if (locations == null || !locations.Any())
            {
                return NotFound("No locations found.");
            }
            return Ok(_mapper.Map<IEnumerable<LocationDTO>>(locations));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Location/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDTO>> GetLocation(int id)
    {
        try
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }
            return Ok(_mapper.Map<LocationDTO>(location));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{cropId}/locations")]
    public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocationsByCrop(int cropId)
    {
        try
        {
            var locations = await _locationRepository.GetLocationsByCropIdAsync(cropId);
            if (locations == null || !locations.Any())
            {
                return NotFound($"No locations found for crop with ID {cropId}.");
            }
            return Ok(_mapper.Map<IEnumerable<LocationDTO>>(locations));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // POST: api/Location
    [HttpPost]
    public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO locationDto)
    {
        try
        {
            var location = _mapper.Map<Location>(locationDto);
            await _locationRepository.AddAsync(location);
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, _mapper.Map<LocationDTO>(location));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // PUT: api/Location/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLocation(int id, LocationDTO locationDto)
    {
        if (id != locationDto.Id)
        {
            return BadRequest("Mismatch between route ID and location ID in body.");
        }

        try
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            _mapper.Map(locationDto, location);
            await _locationRepository.UpdateAsync(location);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // DELETE: api/Location/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        try
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            await _locationRepository.DeleteAsync(location);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
