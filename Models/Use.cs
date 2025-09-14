using System;
using System.Collections.Generic;

namespace AgriWiki_Project.Models;

public partial class Use
{
    public int UseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
}
