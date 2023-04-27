using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? CompanyName { get; set; }

    public bool MailVisible { get; set; }

    public string Email { get; set; } = null!;

    public bool PhoneVisible { get; set; }

    public string? ContactPhone { get; set; }

    public bool IsCompany { get; set; }

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();

    public virtual ICollection<VerificationRequest> VerificationRequests { get; set; } = new List<VerificationRequest>();
}
