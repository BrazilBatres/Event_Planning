using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public sbyte MailVisible { get; set; }

    public string? Email { get; set; }

    public sbyte PhoneVisible { get; set; }

    public string? ContactPhone { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Administrator> Administrators { get; } = new List<Administrator>();

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<Seller> Sellers { get; } = new List<Seller>();
}
