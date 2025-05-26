using Informatics.FiskebodenWebAPI.DTOs;

namespace Informatics.FiskebodenWebAPI.Services.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();

    Task<SupplierDto?> GetSupplierByIdAsync(int id);

}
