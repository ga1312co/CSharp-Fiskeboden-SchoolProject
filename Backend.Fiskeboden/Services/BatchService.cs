using Informatics.FiskebodenWebAPI.Contexts;
using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Extensions;
using Informatics.FiskebodenWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Informatics.FiskebodenWebAPI.Services;

public class BatchService : IBatchService
{
    private readonly FiskeBodenContext _context;

    public BatchService(FiskeBodenContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BatchDto>> GetAllBatchesAsync()
    {
        var batches = await _context.Batches
            .Include(b => b.Supplier)
            .Include(b => b.Product)
            .ToListAsync();

        return batches.Select(b => b.ToDto());
    }

    public async Task<BatchDto?> GetBatchByIdAsync(int id)
    {
        var batch = await _context.Batches
            .Include(b => b.Supplier)
            .Include(b => b.Product)
            .FirstOrDefaultAsync(b => b.BatchId == id);

        return batch?.ToDto();
    }

}
