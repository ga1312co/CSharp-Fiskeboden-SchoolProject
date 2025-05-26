using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Informatics.FiskebodenWebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Informatics.FiskebodenWebAPI.Controllers;


[Route("api/batches")]
[ApiController]
public class BatchesController : ControllerBase
{
    private readonly IBatchService _batchService;

    public BatchesController(IBatchService batchService)
    {
        _batchService = batchService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BatchDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BatchDto>>>GetBatches()
    {
        var batches = await _batchService.GetAllBatchesAsync();
        return Ok(batches);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BatchDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BatchDto>> GetBatch(int id)
    {
        var batch = await _batchService.GetBatchByIdAsync(id);
        if (batch == null)
        {
            return NotFound();
        }
        return Ok(batch);
    }           

}

