using System.IO.Compression;
using AgriWeatherTracker.Data;
using Microsoft.EntityFrameworkCore;

public class GrowthStageRepository : IGrowthStageRepository
{
    private readonly AppDbContext _context;

    public GrowthStageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GrowthStage>> GetAllAsync()
    {
        return await _context.Set<GrowthStage>()
                        .Include(gs => gs.OptimalConditions)
                        .Include(gs => gs.AdverseConditions)
                        .ToListAsync();
    }

    public async Task<GrowthStage> GetByIdAsync(int id)
    {
        return await _context.Set<GrowthStage>().FindAsync(id);
    }

    public async Task AddAsync(GrowthStage growthStage)
    {
        _context.Set<GrowthStage>().Add(growthStage);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GrowthStage growthStage)
    {
        _context.Entry(growthStage).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        if (item != null)
        {
            _context.Set<GrowthStage>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public void SetEntityStateUnchanged<T>(T entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Unchanged;
    }
}