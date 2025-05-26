using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Informatics.FiskebodenWebAPI.Controllers;

[Route("api/supplierpickuppoints")]
[ApiController]
public class SupplierPickupPointsController : ControllerBase
{
    private readonly ISupplierPickupPointService _supplierPickupPointService;

    public SupplierPickupPointsController(ISupplierPickupPointService supplierPickupPointService)
    {
        _supplierPickupPointService = supplierPickupPointService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SupplierPickupPointDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SupplierPickupPointDto>>> GetSupplierPickupPoints()
    {
        var supplierPickupPoints = await _supplierPickupPointService.GetAllSupplierPickupPointsAsync();           
        return Ok(supplierPickupPoints);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SupplierPickupPointDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SupplierPickupPointDto>> GetSupplierPickupPoint(int id)
    {
        var supplierPickupPoint = await _supplierPickupPointService.GetSupplierPickupPointByIdAsync(id);
        if (supplierPickupPoint == null)
        {
            return NotFound();
        }
        return Ok(supplierPickupPoint);
    }
}

