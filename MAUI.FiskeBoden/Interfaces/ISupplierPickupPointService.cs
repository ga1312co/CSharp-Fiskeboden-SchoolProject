using Informatics.FiskeBoden.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.Interfaces
{
    public interface ISupplierPickupPointService
    {
        Task<IEnumerable<SupplierPickupPoint>> GetSupplierPickupPointsAsync();
        Task<SupplierPickupPoint?> GetSupplierPickupPointAsync(int supplierPickupPointId);
        Task<SupplierPickupPoint> CreateSupplierPickupPointAsync(SupplierPickupPoint supplierPickupPoint);
        Task<SupplierPickupPoint> UpdateSupplierPickupPointAsync(SupplierPickupPoint supplierPickupPoint);
        Task DeleteSupplierPickupPointAsync(int supplierPickupPointId);
    }
}
