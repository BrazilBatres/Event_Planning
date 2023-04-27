using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class CatalogItem
{
    public int Id { get; set; }

    public int SellerId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemDescription { get; set; } = null!;

    public decimal ItemPrice { get; set; }

    public int ItemCategoryId { get; set; }

    public sbyte IsService { get; set; }

    public virtual ICollection<EventItem> EventItems { get; set; } = new List<EventItem>();

    public virtual ItemCategory ItemCategory { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
