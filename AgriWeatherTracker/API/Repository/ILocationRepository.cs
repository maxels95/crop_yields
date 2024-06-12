using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllAsync();
    Task<Location> GetByIdAsync(int id);
    Task<IEnumerable<Location>> GetLocationsByCropIdAsync(int cropId);
    Task AddAsync(Location location);
    Task UpdateAsync(Location location);
    Task DeleteAsync(Location location);
}