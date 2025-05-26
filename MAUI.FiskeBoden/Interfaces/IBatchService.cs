using Informatics.FiskeBoden.Models;

namespace Informatics.FiskeBoden.Interfaces;

public interface IBatchService
{
    Task<IEnumerable<Batch>> GetBatchesAsync();
    Task<Batch?> GetBatchAsync(int batchId);
    Task<Batch> CreateBatchAsync(Batch batch);
    Task<Batch> UpdateBatchAsync(Batch batch);
    Task DeleteBatchAsync(int batchId);
}