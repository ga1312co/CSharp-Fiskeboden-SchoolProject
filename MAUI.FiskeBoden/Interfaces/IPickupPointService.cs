using Informatics.FiskeBoden.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.Interfaces
{
    public interface IPickupPointService
    {
        Task<IEnumerable<PickupPoint>> GetPickupPointsAsync();
        Task<PickupPoint?> GetPickupPointAsync(int pickupPointId);
        Task<PickupPoint> CreatePickupPointAsync(PickupPoint pickupPoint);
        Task<PickupPoint> UpdatePickupPointAsync(PickupPoint pickupPoint);
        Task DeletePickupPointAsync(int pickupPointId);
    }
}
