using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class EventItem
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual CatalogItem Item { get; set; } = null!;
}
