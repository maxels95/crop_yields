using AgriWeatherTracker.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[Route("api/[controller]")]
[ApiController]
public class CropController : ControllerBase
{
    private readonly ICropRepository _cropRepository;
    private readonly IMapper _mapper;

    public CropController(ICropRepository cropRepository, IMapper mapper)
    {
        _cropRepository = cropRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Crop>>> GetCrops()
    {
        var crops = await _cropRepository.GetAllCropsAsync();
        return Ok(crops);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Crop>> GetCrop(int id)
    {
        var crop = await _cropRepository.GetCropByIdAsync(id);
        if (crop == null)
        {
            return NotFound();
        }
        return Ok(crop);
    }

    [HttpPost]
    public async Task<ActionResult<Crop>> PostCrop(CropDTO cropDTO)
    {
        var crop = _mapper.Map<Crop>(cropDTO);
        await _cropRepository.CreateCropAsync(crop);
        return CreatedAtAction(nameof(GetCrop), new { id = crop.Id }, crop);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCrop(int id, CropDTO cropDTO)
    {
        if (id != cropDTO.Id)
        {
            return BadRequest();
        }
        var crop = _mapper.Map<Crop>(cropDTO);
        await _cropRepository.UpdateCropAsync(crop);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCrop(int id)
    {
        await _cropRepository.DeleteCropAsync(id);
        return NoContent();
    }
}
