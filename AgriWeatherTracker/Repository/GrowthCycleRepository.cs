using AgriWeatherTracker.Data;
using Microsoft.EntityFrameworkCore;

public class GrowthCycleRepository : IGrowthCycleRepository
{
    private readonly AppDbContext _context;

    public GrowthCycleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GrowthCycle>> GetAllAsync()
    {
        return await _context.Set<GrowthCycle>()
                        .Include(gc => gc.Stages)
                        .ToListAsync();
    }

    public async Task<GrowthCycle> GetByIdAsync(int id)
    {
        return await _context.Set<GrowthCycle>().FindAsync(id);
    }

    public async Task AddAsync(GrowthCycle growthCycle)
    {
        _context.Set<GrowthCycle>().Add(growthCycle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GrowthCycle growthCycle)
    {
        _context.Entry(growthCycle).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        if (item != null)
        {
            _context.Set<GrowthCycle>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public void SetEntityStateUnchanged(GrowthStage entity)
    {

        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _context.GrowthStages.Attach(entity);
        }
        _context.Entry(entity).State = EntityState.Added;
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Unchanged;
    }
}