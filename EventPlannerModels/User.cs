using System;
using System.Collections.Generic;

namespace EventPlannerModels;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Administrator> Administrators { get; } = new List<Administrator>();

    public virtual ICollection<Buyer> Buyers { get; } = new List<Buyer>();

    public virtual ICollection<Seller> Sellers { get; } = new List<Seller>();
}
