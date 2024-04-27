using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GrowthStageController : ControllerBase
{
    private readonly IGrowthStageRepository _repository;
    private readonly IMapper _mapper;

    public GrowthStageController(IGrowthStageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GrowthStageDTO>>> GetAll()
    {
        var stages = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<GrowthStageDTO>>(stages));
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
    public async Task<ActionResult<GrowthStageDTO>> Create(GrowthStageDTO dto)
    {
        var stage = _mapper.Map<GrowthStage>(dto);
        await _repository.AddAsync(stage);
        return CreatedAtAction(nameof(Get), new { id = stage.Id }, _mapper.Map<GrowthStageDTO>(stage));
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
