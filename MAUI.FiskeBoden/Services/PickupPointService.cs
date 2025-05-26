using Informatics.FiskeBoden.Models;
using Informatics.FiskeBoden.Data;
using Informatics.FiskeBoden.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.Services
{
    public class PickupPointService : IPickupPointService
    {
        private readonly AppDbContext _context;

        public PickupPointService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PickupPoint>> GetPickupPointsAsync()
        {
            return await _context.PickupPoints.Include(p => p.SupplierPickupPoints).ToListAsync();
        }

        public async Task<PickupPoint?> GetPickupPointAsync(int pickupPointId)
        {
            return await _context.PickupPoints
                .Include(p => p.SupplierPickupPoints)
                .FirstOrDefaultAsync(p => p.PickupPointId == pickupPointId);
        }

        public async Task<PickupPoint> CreatePickupPointAsync(PickupPoint pickupPoint)
        {
            _context.PickupPoints.Add(pickupPoint);
            await _context.SaveChangesAsync();
            return pickupPoint;
        }

        public async Task<PickupPoint> UpdatePickupPointAsync(PickupPoint pickupPoint)
        {
            _context.PickupPoints.Update(pickupPoint);
            await _context.SaveChangesAsync();
            return pickupPoint;
        }

        public async Task DeletePickupPointAsync(int pickupPointId)
        {
            PickupPoint? pickupPoint = await _context.PickupPoints.FindAsync(pickupPointId);
            if (pickupPoint != null)
            {
                _context.PickupPoints.Remove(pickupPoint);
                await _context.SaveChangesAsync();
            }
        }
    }
}
