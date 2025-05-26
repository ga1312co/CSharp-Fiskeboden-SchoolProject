using Informatics.FiskebodenWebAPI.Contexts;
using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Extensions;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Informatics.FiskebodenWebAPI.Services;

public class SupplierPickupPointService : ISupplierPickupPointService
{
    private readonly FiskeBodenContext _context;

    public SupplierPickupPointService(FiskeBodenContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SupplierPickupPointDto>> GetAllSupplierPickupPointsAsync()
    {
        var supplierPickupPoint = await _context.SupplierPickupPoints
            .Include(spp => spp.Supplier)
            .Include(spp => spp.PickupPoint)
            .ToListAsync();

        return supplierPickupPoint.Select(spp => spp.ToDto());
    }

    public async Task<SupplierPickupPointDto?> GetSupplierPickupPointByIdAsync(int id)
    {
        var supplierPickupPoint = await _context.SupplierPickupPoints
            .Include(spp => spp.Supplier)
            .Include(spp => spp.PickupPoint)
            .FirstOrDefaultAsync(spp => spp.SupplierPickupPointId == id);

        return supplierPickupPoint?.ToDto();
    }

}
