using AgriWeatherTracker.Models;

public interface ICropRepository
{
    Task<IEnumerable<Crop>> GetAllCropsAsync();
    Task<Crop> GetCropByIdAsync(int cropId);
    Task CreateCropAsync(Crop crop);
    Task UpdateCropAsync(Crop crop);
    Task DeleteCropAsync(int cropId);
}

