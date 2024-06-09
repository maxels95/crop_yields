using AgriWeatherTracker.Data;
using AgriWeatherTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class HealthScoreRepository : IHealthScoreRepository
{
    private readonly AppDbContext _context;

    public HealthScoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HealthScore> GetHealthScoreByIdAsync(int id)
    {
        return await _context.HealthScores.Include(hs => hs.Crop).FirstOrDefaultAsync(hs => hs.Id == id);
    }

    public async Task CreateHealthScoreAsync(HealthScore healthScore)
    {
        _context.HealthScores.Add(healthScore);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateHealthScoreAsync(HealthScore healthScore)
    {
        _context.Entry(healthScore).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteHealthScoreAsync(int id)
    {
        var healthScore = await _context.HealthScores.FindAsync(id);
        if (healthScore != null)
        {
            _context.HealthScores.Remove(healthScore);
            await _context.SaveChangesAsync();
        }
    }
}
