using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class Event
{
    public int Id { get; set; }

    public int BuyerId { get; set; }

    public string EventName { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public string EventLocation { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public decimal EventBudget { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual ICollection<EventItem> EventItems { get; set; } = new List<EventItem>();
}
