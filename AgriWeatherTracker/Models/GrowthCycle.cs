using System;
using System.Collections.Generic;

public class GrowthCycle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GrowthStage> Stages { get; set; } = new List<GrowthStage>();
    }