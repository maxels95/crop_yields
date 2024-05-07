public class GrowthStageDTO
{
    public int Id { get; set; }
    public string StageName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int OptimalConditions { get; set; }
    public int AdverseConditions { get; set; }
    public int ResilienceDurationInDays { get; set; }
    public double HistoricalAdverseImpactScore { get; set; }
}

