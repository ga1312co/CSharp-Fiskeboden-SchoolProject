using Informatics.FiskeBoden.Models;
using Informatics.FiskeBoden.Data;
using Microsoft.EntityFrameworkCore;
using Informatics.FiskeBoden.Interfaces;

namespace Informatics.FiskeBoden.Services;

public class BatchService : IBatchService
{
    private readonly AppDbContext _context;

    public BatchService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Batch>> GetBatchesAsync() 
    {
        return await _context.Batches
                             .Include(b => b.Supplier)
                             .Include(b => b.Product)
                             .ToListAsync();
    }

    public async Task<Batch?> GetBatchAsync(int batchId) 
    {
        return await _context.Batches
                             .Include(b => b.Supplier)
                             .Include(b => b.Product)
                             .FirstOrDefaultAsync(b => b.BatchId == batchId);
    }

    public async Task<Batch> CreateBatchAsync(Batch batch) 
    {
        _context.Batches.Add(batch);
        await _context.SaveChangesAsync();
        return batch;
    }

    public async Task<Batch> UpdateBatchAsync(Batch batch) 
    {
        _context.Batches.Update(batch);
        await _context.SaveChangesAsync();
        return batch;
    }

    public async Task DeleteBatchAsync(int batchId) 
    {
        Batch? batch = await _context.Batches.FindAsync(batchId);
        if (batch != null)
        {
            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();
        }
    }
}
