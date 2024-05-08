using AgriWeatherTracker.Data;
using AgriWeatherTracker.Models;
using Microsoft.EntityFrameworkCore;

public class WeatherRepository : IWeatherRepository
{
    private readonly AppDbContext _context;

    public WeatherRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Weather>> GetAllWeatherAsync()
    {
        return await _context.Weathers
                        .Include(w => w.Location)
                        .ThenInclude(l => l.Crop)
                        .ToListAsync();
    }

    public async Task<Weather> GetWeatherByIdAsync(int weatherId)
    {
        return await _context.Weathers.FindAsync(weatherId);
    }

    public async Task<IEnumerable<Weather>> GetWeatherByLocationAsync(int locationId)
    {
        return await _context.Weathers
            .Where(w => w.Location.Id == locationId)
            .ToListAsync();
    }

    public async Task CreateWeatherAsync(Weather weather)
    {
        _context.Weathers.Add(weather);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateWeatherAsync(Weather weather)
    {
        _context.Weathers.Update(weather);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWeatherAsync(int weatherId)
    {
        var weather = await _context.Weathers.FindAsync(weatherId);
        if (weather != null)
        {
            _context.Weathers.Remove(weather);
            await _context.SaveChangesAsync();
        }
    }

    public void SetEntityStateUnchanged<T>(T entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Unchanged;
    }
}
