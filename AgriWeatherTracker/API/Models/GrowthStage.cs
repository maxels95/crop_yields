using System;
using System.Collections.Generic;

public class GrowthStage
    {
        public int Id { get; set; }
        public string StageName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ConditionThreshold OptimalConditions { get; set; }
        public ConditionThreshold AdverseConditions { get; set; }
        public int ResilienceDurationInDays { get; set; }  // Duration the stage can withstand adverse conditions
        public double HistoricalAdverseImpactScore { get; set; }
    }