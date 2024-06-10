using Microsoft.AspNetCore.Mvc;
using AgriWeatherTracker.Models;
using AgriWeatherTracker.Data;
using AutoMapper;

[Route("api/[controller]")]
[ApiController]
public class HealthScoreController : ControllerBase
{
    private readonly IHealthScoreRepository _healthScoreRepository;
    private readonly ICropRepository _cropRepository;
    private readonly IMapper _mapper;

    public HealthScoreController(IHealthScoreRepository healthScoreRepository, ICropRepository cropRepository, IMapper mapper)
    {
        _healthScoreRepository = healthScoreRepository;
        _cropRepository = cropRepository;
        _mapper = mapper;
    }

    // GET: api/HealthScore/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<HealthScoreDto>> GetHealthScore(int id)
    {
        var healthScore = await _healthScoreRepository.GetHealthScoreByIdAsync(id);
        if (healthScore == null)
        {
            return NotFound();
        }
        return _mapper.Map<HealthScoreDto>(healthScore);
    }

    // POST: api/HealthScore
    [HttpPost]
    public async Task<ActionResult<HealthScoreDto>> PostHealthScore(HealthScoreDto healthScoreDto)
    {
        var crop = await _cropRepository.GetCropByIdAsync(healthScoreDto.CropId);
        var healthScore = _mapper.Map<HealthScore>(healthScoreDto);

        if (crop != null) {
                _cropRepository.SetEntityStateUnchanged(crop);
                healthScore.Crop = crop;
        }

        await _healthScoreRepository.CreateHealthScoreAsync(healthScore);
        return CreatedAtAction(nameof(GetHealthScore), new { id = healthScore.Id }, _mapper.Map<HealthScoreDto>(healthScore));
    }

    // PUT: api/HealthScore/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHealthScore(int id, double score)
    {
        var healthScore = await _healthScoreRepository.GetHealthScoreByIdAsync(id);
        if (healthScore == null)
        {
            return NotFound();
        }

        healthScore.Score = score;
        healthScore.Date = DateTime.Now;
        await _healthScoreRepository.UpdateHealthScoreAsync(healthScore);
        return NoContent();
    }
}
