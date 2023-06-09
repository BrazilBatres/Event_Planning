﻿using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class VerificationStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VerificationRequest> VerificationRequests { get; set; } = new List<VerificationRequest>();
}
