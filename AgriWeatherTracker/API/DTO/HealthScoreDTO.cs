public class HealthScoreDto
    {
        public int Id { get; set; }
        public int CropId { get; set; }  // Foreign key to Crop
        public DateTime Date { get; set; }
        public double Score { get; set; }
    }