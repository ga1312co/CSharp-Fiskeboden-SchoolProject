using Informatics.FiskebodenWebAPI.Contexts;
using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Extensions;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Informatics.FiskebodenWebAPI.Services;

public class PickupPointService : IPickupPointService
{
    private readonly FiskeBodenContext _context;

    public PickupPointService(FiskeBodenContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PickupPointDto>> GetAllPickupPointsAsync()
    {
        var pickupPoints =  await _context.PickupPoints
            .Include(pp => pp.SupplierPickupPoints)
            .ToListAsync();

        return pickupPoints.Select(pp => pp.ToDto());
    }

    public async Task<PickupPointDto?> GetPickupPointByIdAsync(int id)
    {
        var pickupPoint = await _context.PickupPoints
            .Include(pp => pp.SupplierPickupPoints)
            .FirstOrDefaultAsync(pp => pp.PickupPointId == id);

        return pickupPoint?.ToDto();
    }
}
