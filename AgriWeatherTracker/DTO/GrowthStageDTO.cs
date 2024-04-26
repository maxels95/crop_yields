public class GrowthStageDTO
{
    public int Id { get; set; }
    public string StageName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ConditionThresholdDTO OptimalConditions { get; set; }
    public ConditionThresholdDTO AdverseConditions { get; set; }
    public int ResilienceDurationInDays { get; set; }
}
