using System;
using System.Collections.Generic;

namespace AgriWiki_Project.Models;

public partial class Disease 
{
    public int DiseaseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Prevention { get; set; }

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
}
