using Informatics.FiskeBoden.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
        Task<Supplier?> GetSupplierAsync(int supplierId);
        Task<Supplier> CreateSupplierAsync(Supplier supplier);
        Task<Supplier> UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(int supplierId);
    }
}
