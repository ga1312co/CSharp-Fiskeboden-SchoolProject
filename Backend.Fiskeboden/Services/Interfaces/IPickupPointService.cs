using Informatics.FiskebodenWebAPI.DTOs;

namespace Informatics.FiskebodenWebAPI.Services.Interfaces;

public interface IPickupPointService
{
    Task<IEnumerable<PickupPointDto>> GetAllPickupPointsAsync();

    Task<PickupPointDto?> GetPickupPointByIdAsync(int id);

}
