public interface IHealthScoreRepository
{
    Task<HealthScore> GetHealthScoreByIdAsync(int id);
    Task<IEnumerable<HealthScore>> GetHealthScoresByCropIdAsync(int cropId);
    Task CreateHealthScoreAsync(HealthScore healthScore);
    Task UpdateHealthScoreAsync(HealthScore healthScore);
    Task DeleteHealthScoreAsync(int id);
}
