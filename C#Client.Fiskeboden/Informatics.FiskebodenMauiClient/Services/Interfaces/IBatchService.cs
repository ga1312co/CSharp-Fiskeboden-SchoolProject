using System;
using Informatics.FiskebodenMauiClient.Models;

namespace Informatics.FiskebodenMauiClient.Services;

public interface IBatchService
{
    Task<IEnumerable<Batch>> GetBatchesAsync();
}
