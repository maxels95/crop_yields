public interface IGrowthCycleRepository
{
    Task<IEnumerable<GrowthCycle>> GetAllAsync();
    Task<GrowthCycle> GetByIdAsync(int id);
    Task AddAsync(GrowthCycle growthCycle);
    Task UpdateAsync(GrowthCycle growthCycle);
    Task DeleteAsync(int id);
}