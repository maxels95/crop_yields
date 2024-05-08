using AgriWeatherTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


public class LocationRepository : ILocationRepository
{
    private readonly AppDbContext _context;

    public LocationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location> GetByIdAsync(int id)
    {
        return await _context.Locations.FindAsync(id);
    }

    public async Task AddAsync(Location location)
    {
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Location location)
    {
        _context.Entry(location).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LocationExists(location.Id))
            {
                throw new KeyNotFoundException("Location not found.");
            }
            else
            {
                throw;
            }
        }
    }

    public async Task DeleteAsync(Location location)
    {
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
    }

    private bool LocationExists(int id)
    {
        return _context.Locations.Any(e => e.Id == id);
    }
}
