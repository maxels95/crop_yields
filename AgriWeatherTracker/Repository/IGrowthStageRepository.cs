public interface IGrowthStageRepository
{
    Task<IEnumerable<GrowthStage>> GetAllAsync();
    Task<GrowthStage> GetByIdAsync(int id);
    Task AddAsync(GrowthStage growthStage);
    Task UpdateAsync(GrowthStage growthStage);
    Task DeleteAsync(int id);
}