using Microsoft.EntityFrameworkCore;

public class GrowthCycleRepository : IGrowthCycleRepository
{
    private readonly DbContext _context;

    public GrowthCycleRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GrowthCycle>> GetAllAsync()
    {
        return await _context.Set<GrowthCycle>().ToListAsync();
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
}