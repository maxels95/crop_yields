using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ConditionThresholdController : ControllerBase
{
    private readonly IConditionThresholdRepository _repository;
    private readonly IMapper _mapper;

    public ConditionThresholdController(IConditionThresholdRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConditionThresholdDTO>>> GetAll()
    {
        try
        {
            var items = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ConditionThresholdDTO>>(items));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving condition thresholds.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConditionThresholdDTO>> Get(int id)
    {
        try
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound($"ConditionThreshold with ID {id} not found.");
            }
            return Ok(_mapper.Map<ConditionThresholdDTO>(item));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the condition threshold with ID {id}.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ConditionThresholdDTO>> Create(ConditionThresholdDTO dto)
    {
        try
        {
            var item = _mapper.Map<ConditionThreshold>(dto);
            await _repository.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, _mapper.Map<ConditionThresholdDTO>(item));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while creating the condition threshold.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ConditionThresholdDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch in the request.");
        }

        try
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound($"ConditionThreshold with ID {id} not found.");
            }

            _mapper.Map(dto, item);
            await _repository.UpdateAsync(item);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the condition threshold with ID {id}.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound($"ConditionThreshold with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the condition threshold with ID {id}.");
        }
    }
}
