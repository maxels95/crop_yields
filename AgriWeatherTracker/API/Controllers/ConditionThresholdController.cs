using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ConditionThresholdController : ControllerBase
{
    private readonly IConditionThresholdRepository _repository;
    private readonly IMapper _mapper;

    public ConditionThresholdController(IConditionThresholdRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConditionThresholdDTO>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ConditionThresholdDTO>>(items));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConditionThresholdDTO>> Get(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ConditionThresholdDTO>(item));
    }

    [HttpPost]
    public async Task<ActionResult<ConditionThresholdDTO>> Create(ConditionThresholdDTO dto)
    {
        var item = _mapper.Map<ConditionThreshold>(dto);
        await _repository.AddAsync(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, _mapper.Map<ConditionThresholdDTO>(item));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ConditionThresholdDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _mapper.Map(dto, item);
        await _repository.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
