public class HealthScore
{
    public int Id { get; set; }  // Primary key for HealthScore
    public int CropId { get; set; }  // Foreign key to Crop
    public Crop Crop { get; set; }
    public DateTime Date { get; set; }
    public double Score { get; set; }
}
