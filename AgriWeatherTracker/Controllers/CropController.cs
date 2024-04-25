using AgriWeatherTracker.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CropController : ControllerBase
{
    private readonly ICropRepository _cropRepository;

    public CropController(ICropRepository cropRepository)
    {
        _cropRepository = cropRepository;
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
    public async Task<ActionResult<Crop>> PostCrop(Crop crop)
    {
        await _cropRepository.CreateCropAsync(crop);
        return CreatedAtAction(nameof(GetCrop), new { id = crop.Id }, crop);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCrop(int id, Crop crop)
    {
        if (id != crop.Id)
        {
            return BadRequest();
        }
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
