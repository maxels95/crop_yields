using AgriWeatherTracker.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CropController : ControllerBase
{
    private readonly ICropRepository _cropRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IGrowthCycleRepository _growthCycleRepository;
    private readonly IMapper _mapper;

    public CropController(ICropRepository cropRepository, IMapper mapper, 
        ILocationRepository locationRepository, IGrowthCycleRepository growthCycleRepository)
    {
        _cropRepository = cropRepository ?? throw new ArgumentNullException(nameof(cropRepository));
        _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        _growthCycleRepository = growthCycleRepository ?? throw new ArgumentNullException(nameof(growthCycleRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Crop>>> GetCrops()
    {
        try
        {
            var crops = await _cropRepository.GetAllCropsAsync();
            return Ok(crops);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving crops.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Crop>> GetCrop(int id)
    {
        try
        {
            var crop = await _cropRepository.GetCropByIdAsync(id);
            if (crop == null)
            {
                return NotFound($"No crop found with ID {id}.");
            }
            return Ok(crop);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the crop with ID {id}.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Crop>> PostCrop(CropDTO cropDTO)
    {
        try
        {
            var locations = new List<Location>();
            var growthCycles = new List<GrowthCycle>();

            foreach (int id in cropDTO.Locations)
            {
                var location = await _locationRepository.GetByIdAsync(id);
                if (location != null)
                {
                    _cropRepository.SetEntityStateUnchanged(location);
                    locations.Add(location);
                }
            }

            foreach (int id in cropDTO.GrowthCycles)
            {
                var growthCycle = await _growthCycleRepository.GetByIdAsync(id);
                if (growthCycle != null)
                {
                    _cropRepository.SetEntityStateUnchanged(growthCycle);
                    growthCycles.Add(growthCycle);
                }
            }

            var crop = _mapper.Map<Crop>(cropDTO);
            crop.Locations = locations;
            crop.GrowthCycles = growthCycles;

            await _cropRepository.CreateCropAsync(crop);
            return CreatedAtAction(nameof(GetCrop), new { id = crop.Id }, crop);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while creating the crop.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCrop(int id, CropDTO cropDTO)
    {
        if (id != cropDTO.Id)
        {
            return BadRequest("ID mismatch in the request.");
        }

        try
        {
            var crop = await _cropRepository.GetCropByIdAsync(id);
            if (crop == null)
            {
                return NotFound($"No crop found with ID {id}.");
            }

            _mapper.Map(cropDTO, crop);
            await _cropRepository.UpdateCropAsync(crop);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the crop with ID {id}.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCrop(int id)
    {
        try
        {
            var crop = await _cropRepository.GetCropByIdAsync(id);
            if (crop == null)
            {
                return NotFound($"No crop found with ID {id}.");
            }

            await _cropRepository.DeleteCropAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the crop with ID {id}.");
        }
    }
}
