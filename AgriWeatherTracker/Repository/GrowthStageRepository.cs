using Microsoft.EntityFrameworkCore;

public class GrowthStageRepository : IGrowthStageRepository
{
    private readonly DbContext _context;

    public GrowthStageRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GrowthStage>> GetAllAsync()
    {
        return await _context.Set<GrowthStage>().ToListAsync();
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
}