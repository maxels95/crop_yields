using AgriWeatherTracker.Models;

public interface IWeatherRepository
{
    Task<IEnumerable<Weather>> GetAllWeatherAsync();
    Task<Weather> GetWeatherByIdAsync(int weatherId);
    Task<IEnumerable<Weather>> GetWeatherByCropAndLocationAsync(int cropId, int locationId);
    Task CreateWeatherAsync(Weather weather);
    Task UpdateWeatherAsync(Weather weather);
    Task DeleteWeatherAsync(int weatherId);
}