using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class GrowthStageController : ControllerBase
{
    private readonly IGrowthStageRepository _repository;
    private readonly IConditionThresholdRepository _conditionThresholdRepository;
    private readonly IMapper _mapper;

    public GrowthStageController(IGrowthStageRepository repository, IMapper mapper, IConditionThresholdRepository conditionThresholdRepository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _conditionThresholdRepository = conditionThresholdRepository ?? throw new ArgumentNullException(nameof(conditionThresholdRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GrowthStage>>> GetAll()
    {
        try
        {
            var stages = await _repository.GetAllAsync();
            return Ok(stages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving growth stages.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GrowthStageDTO>> Get(int id)
    {
        try
        {
            var stage = await _repository.GetByIdAsync(id);
            if (stage == null)
            {
                return NotFound($"No growth stage found with ID {id}.");
            }
            return Ok(_mapper.Map<GrowthStageDTO>(stage));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the growth stage with ID {id}.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<GrowthStage>> Create(GrowthStageDTO dto)
    {
        try
        {
            var optimalThresholds = await _conditionThresholdRepository.GetByIdAsync(dto.OptimalConditions);
            var adverseThresholds = await _conditionThresholdRepository.GetByIdAsync(dto.AdverseConditions);
            var stage = _mapper.Map<GrowthStage>(dto);

            if (optimalThresholds != null && adverseThresholds != null)
            {
                _repository.SetEntityStateUnchanged(optimalThresholds);
                _repository.SetEntityStateUnchanged(adverseThresholds);
                stage.OptimalConditions = optimalThresholds;
                stage.AdverseConditions = adverseThresholds;
            }

            await _repository.AddAsync(stage);
            return CreatedAtAction(nameof(Get), new { id = stage.Id }, stage);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while creating the growth stage.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GrowthStageDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("Mismatched ID in request.");
        }

        try
        {
            var stage = await _repository.GetByIdAsync(id);
            if (stage == null)
            {
                return NotFound($"No growth stage found with ID {id}.");
            }

            _mapper.Map(dto, stage);
            await _repository.UpdateAsync(stage);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the growth stage with ID {id}.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var stage = await _repository.GetByIdAsync(id);
            if (stage == null)
            {
                return NotFound($"No growth stage found with ID {id}.");
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the growth stage with ID {id}.");
        }
    }
}
