using AgriWeatherTracker.Models;

public interface IWeatherRepository
{
    Task<IEnumerable<Weather>> GetAllWeatherAsync();
    Task<Weather> GetWeatherByIdAsync(int weatherId);
    Task<IEnumerable<Weather>> GetWeatherByLocationAsync(int locationId);
    Task CreateWeatherAsync(Weather weather);
    Task UpdateWeatherAsync(Weather weather);
    Task DeleteWeatherAsync(int weatherId);
    void SetEntityStateUnchanged<T>(T entity);
    Task<List<Weather>> GetWeatherData(int cropId, int locationId, DateTime startDate, DateTime endDate);
}