public interface IHealthScoreRepository
{
    Task<HealthScore> GetHealthScoreByIdAsync(int id);
    Task CreateHealthScoreAsync(HealthScore healthScore);
    Task UpdateHealthScoreAsync(HealthScore healthScore);
    Task DeleteHealthScoreAsync(int id);
}
