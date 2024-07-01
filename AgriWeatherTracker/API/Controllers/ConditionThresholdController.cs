using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ConditionThresholdController : ControllerBase
{
    private readonly IConditionThresholdRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<ConditionThresholdController> _logger;

    public ConditionThresholdController(IConditionThresholdRepository repository, IMapper mapper, ILogger<ConditionThresholdController> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConditionThresholdDTO>>> GetAll()
    {
        _logger.LogInformation("Getting all condition thresholds.");

        try
        {
            var items = await _repository.GetAllAsync();
            _logger.LogInformation("Successfully retrieved all condition thresholds.");
            return Ok(_mapper.Map<IEnumerable<ConditionThresholdDTO>>(items));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving condition thresholds.");
            return StatusCode(500, "An error occurred while retrieving condition thresholds.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConditionThresholdDTO>> Get(int id)
    {
        _logger.LogInformation($"Getting condition threshold with ID {id}.");

        try
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                _logger.LogWarning($"ConditionThreshold with ID {id} not found.");
                return NotFound($"ConditionThreshold with ID {id} not found.");
            }
            _logger.LogInformation($"Successfully retrieved condition threshold with ID {id}.");
            return Ok(_mapper.Map<ConditionThresholdDTO>(item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while retrieving the condition threshold with ID {id}.");
            return StatusCode(500, $"An error occurred while retrieving the condition threshold with ID {id}.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ConditionThresholdDTO>> Create(ConditionThresholdDTO dto)
    {
        _logger.LogInformation("Creating a new condition threshold.");

        try
        {
            var item = _mapper.Map<ConditionThreshold>(dto);
            await _repository.AddAsync(item);
            _logger.LogInformation($"Successfully created condition threshold with ID {item.Id}.");
            return CreatedAtAction(nameof(Get), new { id = item.Id }, _mapper.Map<ConditionThresholdDTO>(item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the condition threshold.");
            return StatusCode(500, "An error occurred while creating the condition threshold.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ConditionThresholdDTO dto)
    {
        if (id != dto.Id)
        {
            _logger.LogWarning("ID mismatch in the request.");
            return BadRequest("ID mismatch in the request.");
        }

        _logger.LogInformation($"Updating condition threshold with ID {id}.");

        try
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                _logger.LogWarning($"ConditionThreshold with ID {id} not found.");
                return NotFound($"ConditionThreshold with ID {id} not found.");
            }

            _mapper.Map(dto, item);
            await _repository.UpdateAsync(item);
            _logger.LogInformation($"Successfully updated condition threshold with ID {id}.");
            return NoC