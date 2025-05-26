using Informatics.FiskebodenWebAPI.DTOs;

namespace Informatics.FiskebodenWebAPI.Services.Interfaces;

public interface IBatchService
{
    Task<IEnumerable<BatchDto>> GetAllBatchesAsync();

    Task<BatchDto?> GetBatchByIdAsync(int id);

}
