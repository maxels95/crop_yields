using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class GrowthCycleController : ControllerBase
{
    private readonly IGrowthCycleRepository _repository;
    private readonly IMapper _mapper;

    public GrowthCycleController(IGrowthCycleRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GrowthCycleDTO>>> GetAll()
    {
        try
        {
            var cycles = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<GrowthCycleDTO>>(cycles));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving growth cycles.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GrowthCycleDTO>> Get(int id)
    {
        try
        {
            var cycle = await _repository.GetByIdAsync(id);
            if (cycle == null)
            {
                return NotFound($"No growth cycle found with ID {id}.");
            }
            return Ok(_mapper.Map<GrowthCycleDTO>(cycle));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the growth cycle with ID {id}.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<GrowthCycleDTO>> Create(GrowthCycleDTO dto)
    {
        try
        {
            var cycle = _mapper.Map<GrowthCycle>(dto);
            foreach (var stage in cycle.Stages)
            {
                _repository.SetEntityStateUnchanged(stage);
            }
            await _repository.AddAsync(cycle);
            return CreatedAtAction(nameof(Get), new { id = cycle.Id }, _mapper.Map<GrowthCycleDTO>(cycle));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while creating the growth cycle.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GrowthCycleDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("Mismatched ID in request.");
        }

        try
        {
            var cycle = await _repository.GetByIdAsync(id);
            if (cycle == null)
            {
                return NotFound($"No growth cycle found with ID {id}.");
            }
            _mapper.Map(dto, cycle);
            await _repository.UpdateAsync(cycle);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the growth cycle with ID {id}.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var cycle = await _repository.GetByIdAsync(id);
            if (cycle == null)
            {
                return NotFound($"No growth cycle found with ID {id}.");
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the growth cycle with ID {id}.");
        }
    }
}
