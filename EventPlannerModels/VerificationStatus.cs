using System;
using System.Collections.Generic;

namespace EventPlannerModels;

public partial class VerificationStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VerificationRequest> VerificationRequests { get; } = new List<VerificationRequest>();
}
