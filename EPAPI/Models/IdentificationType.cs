using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class IdentificationType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Seller> Sellers { get; } = new List<Seller>();
}
