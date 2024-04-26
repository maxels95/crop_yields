using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationController(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    // GET: api/Location
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAllLocations()
    {
        var locations = await _locationRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<LocationDTO>>(locations));
    }

    // GET: api/Location/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDTO>> GetLocation(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<LocationDTO>(location));
    }

    // POST: api/Location
    [HttpPost]
    public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO locationDto)
    {
        var location = _mapper.Map<Location>(locationDto);
        await _locationRepository.AddAsync(location);
        return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, _mapper.Map<LocationDTO>(location));
    }

    // PUT: api/Location/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLocation(int id, LocationDTO locationDto)
    {
        if (id != locationDto.Id)
        {
            return BadRequest();
        }

        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null)
        {
            return NotFound();
        }

        _mapper.Map(locationDto, location);
        await _locationRepository.UpdateAsync(location);
        return NoContent();
    }

    // DELETE: api/Location/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null)
        {
            return NotFound();
        }

        await _locationRepository.DeleteAsync(location);
        return NoContent();
    }
}
