using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class ItemCategory
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
}
