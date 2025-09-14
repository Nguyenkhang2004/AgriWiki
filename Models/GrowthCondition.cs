using System;
using System.Collections.Generic;

namespace AgriWiki_Project.Models;

public partial class GrowthCondition
{
    public int ConditionId { get; set; }

    public int? PlantId { get; set; }

    public string? TemperatureRange { get; set; }

    public string? HumidityRange { get; set; }

    public string? SoilType { get; set; }

    public string? Sunlight { get; set; }

    public string? Watering { get; set; }

    public virtual Plant? Plant { get; set; }
}
