using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class GrowthStageController : ControllerBase
{
    private readonly IGrowthStageRepository _repository;
    private readonly IConditionThresholdRepository _conditionThresholdRepository;
    private readonly IMapper _mapper;

    public GrowthStageController(IGrowthStageRepository repository, IMapper mapper, IConditionThresholdRepository conditionThresholdRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _conditionThresholdRepository = conditionThresholdRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GrowthStage>>> GetAll()
    {
        var stages = await _repository.GetAllAsync();
        return Ok(stages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GrowthStageDTO>> Get(int id)
    {
        var stage = await _repository.GetByIdAsync(id);
        if (stage == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<GrowthStageDTO>(stage));
    }

    [HttpPost]
    public async Task<ActionResult<GrowthStage>> Create(GrowthStageDTO dto)
    {
        var optimalThresholds = await _conditionThresholdRepository.GetByIdAsync(dto.OptimalConditions);
        var adverseThresholds = await _conditionThresholdRepository.GetByIdAsync(dto.AdverseConditions);
        var stage = _mapper.Map<GrowthStage>(dto);


        if (optimalThresholds != null && adverseThresholds != null) {
            _repository.SetEntityStateUnchanged(optimalThresholds);
            _repository.SetEntityStateUnchanged(adverseThresholds);
            stage.OptimalConditions = optimalThresholds;
            stage.AdverseConditions = adverseThresholds;
        }

        await _repository.AddAsync(stage);
        return CreatedAtAction(nameof(Get), new { id = stage.Id }, stage);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GrowthStageDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var stage = await _repository.GetByIdAsync(id);
        if (stage == null)
        {
            return NotFound();
        }

        _mapper.Map(dto, stage);
        await _repository.UpdateAsync(stage);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var stage = await _repository.GetByIdAsync(id);
        if (stage == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
