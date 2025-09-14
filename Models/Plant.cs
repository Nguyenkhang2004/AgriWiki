using System;
using System.Collections.Generic;

namespace AgriWiki_Project.Models;

public partial class Plant
{
    public int PlantId { get; set; }

    public string Name { get; set; } = null!;

    public string? ScientificName { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<GrowthCondition> GrowthConditions { get; set; } = new List<GrowthCondition>();

    public virtual ICollection<Disease> Diseases { get; set; } = new List<Disease>();

    public virtual ICollection<Pest> Pests { get; set; } = new List<Pest>();

    public virtual ICollection<Use> Uses { get; set; } = new List<Use>();
}
