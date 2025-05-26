using System.Threading.Tasks;
using System.Collections.Generic;
using Informatics.FiskeBoden.Models;

namespace Informatics.FiskeBoden.Interfaces
{
    public interface IChatGptService
    {
        Task<string> GenerateAdForWeek(int batchWeek, List<Batch> batches);
    }
}