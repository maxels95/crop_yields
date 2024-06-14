public interface IHealthScoreRepository
{
    Task<HealthScore> GetHealthScoreByIdAsync(int id);
    Task<IEnumerable<HealthScore>> GetAllHealthScoresAsync();
    Task<HealthScore> GetHealthScoreByLocationIdAsync(int locationId);
    Task CreateHealthScoreAsync(HealthScore healthScore);
    Task UpdateHealthScoreAsync(HealthScore healthScore);
    Task DeleteHealthScoreAsync(int id);
}
