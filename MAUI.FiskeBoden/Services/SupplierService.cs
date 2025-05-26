using Informatics.FiskeBoden.Models;
using Informatics.FiskeBoden.Data;
using Informatics.FiskeBoden.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;

        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            var suppliers = await _context.Suppliers
                .Include(s => s.Batches)
                .ThenInclude(b => b.Product)
                .Include(s => s.SupplierPickupPoints)
                .ThenInclude(spp => spp.PickupPoint) 
                .ToListAsync();

            Console.WriteLine($"📢 Database returned {suppliers.Count()} suppliers.");
            foreach (Supplier? supplier in suppliers)
            {
                Console.WriteLine($"✅ Supplier i DB: {supplier.SupplierName}");
            }

            return suppliers;
        }



        public async Task<Supplier?> GetSupplierAsync(int supplierId)
        {
            return await _context.Suppliers
                .Include(s => s.Batches)
                .Include(s => s.SupplierPickupPoints)
                .FirstOrDefaultAsync(s => s.SupplierId == supplierId);
        }

        public async Task<Supplier> CreateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> UpdateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task DeleteSupplierAsync(int supplierId)
        {
            Supplier? supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }
    }


}

