using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class Referral
{
    public int Id { get; set; }

    public int SellerId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
