using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Informatics.FiskebodenWebAPI.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SupplierDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers()
    {
        var suppliers = await _supplierService.GetAllSuppliersAsync();
        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(id);
        if (supplier == null)
        {
            return NotFound();
        }
        return Ok(supplier);
    }
}

