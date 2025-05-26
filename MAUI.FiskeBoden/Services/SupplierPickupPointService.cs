using Informatics.FiskeBoden.Models;
using Informatics.FiskeBoden.Data;
using Informatics.FiskeBoden.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.Services
{
    public class SupplierPickupPointService : ISupplierPickupPointService
    {
        private readonly AppDbContext _context;

        public SupplierPickupPointService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplierPickupPoint>> GetSupplierPickupPointsAsync()
        {
            return await _context.SupplierPickupPoints
                .Include(spp => spp.PickupPoint)
                .Include(spp => spp.Supplier)
                .ToListAsync();
        }

        public async Task<SupplierPickupPoint?> GetSupplierPickupPointAsync(int supplierPickupPointId)
        {
            return await _context.SupplierPickupPoints
                .Include(spp => spp.PickupPoint)
                .Include(spp => spp.Supplier)
                .FirstOrDefaultAsync(spp => spp.SupplierPickupPointId == supplierPickupPointId);
        }

        public async Task<SupplierPickupPoint> CreateSupplierPickupPointAsync(SupplierPickupPoint supplierPickupPoint)
        {
            _context.SupplierPickupPoints.Add(supplierPickupPoint);
            await _context.SaveChangesAsync();
            return supplierPickupPoint;
        }

        public async Task<SupplierPickupPoint> UpdateSupplierPickupPointAsync(SupplierPickupPoint supplierPickupPoint)
        {
            _context.SupplierPickupPoints.Update(supplierPickupPoint);
            await _context.SaveChangesAsync();
            return supplierPickupPoint;
        }

        public async Task DeleteSupplierPickupPointAsync(int supplierPickupPointId)
        {
            SupplierPickupPoint? supplierPickupPoint = await _context.SupplierPickupPoints.FindAsync(supplierPickupPointId);
            if (supplierPickupPoint != null)
            {
                _context.SupplierPickupPoints.Remove(supplierPickupPoint);
                await _context.SaveChangesAsync();
            }
        }
    }
}

