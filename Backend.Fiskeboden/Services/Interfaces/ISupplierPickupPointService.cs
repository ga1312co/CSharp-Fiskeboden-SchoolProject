using Informatics.FiskebodenWebAPI.DTOs;

namespace Informatics.FiskebodenWebAPI.Services.Interfaces;

public interface ISupplierPickupPointService
{
    Task<IEnumerable<SupplierPickupPointDto>> GetAllSupplierPickupPointsAsync();

    Task<SupplierPickupPointDto?> GetSupplierPickupPointByIdAsync(int id);
}
