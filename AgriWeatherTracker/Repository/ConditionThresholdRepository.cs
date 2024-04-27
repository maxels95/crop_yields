using Microsoft.EntityFrameworkCore;

public class ConditionThresholdRepository : IConditionThresholdRepository
{
    private readonly DbContext _context;

    public ConditionThresholdRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ConditionThreshold>> GetAllAsync()
    {
        return await _context.Set<ConditionThreshold>().ToListAsync();
    }

    public async Task<ConditionThreshold> GetByIdAsync(int id)
    {
        return await _context.Set<ConditionThreshold>().FindAsync(id);
    }

    public async Task AddAsync(ConditionThreshold conditionThreshold)
    {
        _context.Set<ConditionThreshold>().Add(conditionThreshold);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ConditionThreshold conditionThreshold)
    {
        _context.Entry(conditionThreshold).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        if (item != null)
        {
            _context.Set<ConditionThreshold>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}