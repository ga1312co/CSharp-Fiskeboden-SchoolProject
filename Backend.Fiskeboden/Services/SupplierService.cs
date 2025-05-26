using Informatics.FiskebodenWebAPI.Contexts;
using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Extensions;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Informatics.FiskebodenWebAPI.Services;

public class SupplierService : ISupplierService
{
    private readonly FiskeBodenContext _context;

    public SupplierService(FiskeBodenContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
    {
        var suppliers =  await _context.Suppliers
            .Include(s => s.Batches)
            .Include(s => s.SupplierPickupPoints)
            .ToListAsync();

        return suppliers.Select(s => s.ToDto());
    }

    public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
    {
        var supplier = await _context.Suppliers
            .Include(s => s.Batches)
            .Include (s => s.SupplierPickupPoints)
            .FirstOrDefaultAsync(s => s.SupplierId == id);

        return supplier?.ToDto();
    }

}
