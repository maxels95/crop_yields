using AgriWeatherTracker.Data;
using AgriWeatherTracker.Models;
using Microsoft.EntityFrameworkCore;

public class CropRepository : ICropRepository
{
    private readonly AppDbContext _context;

    public CropRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Crop>> GetAllCropsAsync()
    {
        return await _context.Crops
                        .Include(c => c.GrowthCycles)
                        .ThenInclude(gc => gc.Stages)
                        .ThenInclude(ct => ct.AdverseConditions)
                        .Include(c => c.GrowthCycles)
                        .ThenInclude(gc => gc.Stages)
                        .ThenInclude(ct => ct.OptimalConditions)
                        .Include(c => c.Locations)
                        .ToListAsync();
    }

    public async Task<Crop> GetCropByIdAsync(int cropId)
    {
        return await _context.Crops.FindAsync(cropId);
    }

    public async Task CreateCropAsync(Crop crop)
    {
        _context.Crops.Add(crop);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCropAsync(Crop crop)
    {
        _context.Crops.Update(crop);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCropAsync(int cropId)
    {
        var crop = await _context.Crops.FindAsync(cropId);
        _context.Crops.Remove(crop);
        await _context.SaveChangesAsync();
    }

    public void SetEntityStateUnchanged<T>(T entity)
    {
        _context.Entry(entity).State = EntityState.Unchanged;
    }
}