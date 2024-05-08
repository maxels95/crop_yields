public interface IConditionThresholdRepository
{
    Task<IEnumerable<ConditionThreshold>> GetAllAsync();
    Task<ConditionThreshold> GetByIdAsync(int id);
    Task AddAsync(ConditionThreshold conditionThreshold);
    Task UpdateAsync(ConditionThreshold conditionThreshold);
    Task DeleteAsync(int id);
}