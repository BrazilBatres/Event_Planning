using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? CompanyName { get; set; }

    public sbyte MailVisible { get; set; }

    public string Email { get; set; } = null!;

    public sbyte PhoneVisible { get; set; }

    public string? ContactPhone { get; set; }

    public sbyte IsCompany { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<Seller> Sellers { get; } = new List<Seller>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();

    public virtual ICollection<VerificationRequest> VerificationRequests { get; } = new List<VerificationRequest>();
}
