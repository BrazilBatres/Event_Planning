using System;
using System.Collections.Generic;

namespace EventPlannerModels;

public partial class VerificationRequest
{
    public int Id { get; set; }

    public int SellerId { get; set; }

    public int? AdminId { get; set; }

    public string? Description { get; set; }

    public string? AdminComments { get; set; }

    public int StatusId { get; set; }

    public DateTime TransacDate { get; set; }

    public virtual Administrator? Admin { get; set; }

    public virtual Seller Seller { get; set; } = null!;

    public virtual VerificationStatus Status { get; set; } = null!;
}
