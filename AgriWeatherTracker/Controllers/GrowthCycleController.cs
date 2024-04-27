using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GrowthCycleController : ControllerBase
{
    private readonly IGrowthCycleRepository _repository;
    private readonly IMapper _mapper;

    public GrowthCycleController(IGrowthCycleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GrowthCycleDTO>>> GetAll()
    {
        var cycles = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<GrowthCycleDTO>>(cycles));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GrowthCycleDTO>> Get(int id)
    {
        var cycle = await _repository.GetByIdAsync(id);
        if (cycle == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<GrowthCycleDTO>(cycle));
    }

    [HttpPost]
    public async Task<ActionResult<GrowthCycleDTO>> Create(GrowthCycleDTO dto)
    {
        var cycle = _mapper.Map<GrowthCycle>(dto);
        await _repository.AddAsync(cycle);
        return CreatedAtAction(nameof(Get), new { id = cycle.Id }, _mapper.Map<GrowthCycleDTO>(cycle));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GrowthCycleDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var cycle = await _repository.GetByIdAsync(id);
        if (cycle == null)
        {
            return NotFound();
        }

        _mapper.Map(dto, cycle);
        await _repository.UpdateAsync(cycle);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cycle = await _repository.GetByIdAsync(id);
        if (cycle == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
