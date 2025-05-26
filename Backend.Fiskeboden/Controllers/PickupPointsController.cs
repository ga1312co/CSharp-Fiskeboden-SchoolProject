using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Informatics.FiskebodenWebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Informatics.FiskebodenWebAPI.Controllers;

[Route("api/pickuppoints")]
[ApiController]
public class PickupPointsController : ControllerBase
{
    private readonly IPickupPointService _pickupPointService;

    public PickupPointsController(IPickupPointService pickupPointService)
    {
        _pickupPointService = pickupPointService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PickupPointDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PickupPointDto>>> GetPickupPoints()
    {
        var pickupPoints = await _pickupPointService.GetAllPickupPointsAsync();
        return Ok(pickupPoints);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PickupPointDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PickupPointDto>> GetPickupPoint(int id)
    {
        var pickupPoint = await _pickupPointService.GetPickupPointByIdAsync(id);
        if (pickupPoint == null)
        {
            return NotFound();
        }
        return Ok(pickupPoint);
    }
}

