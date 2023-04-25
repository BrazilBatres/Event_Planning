using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class Seller
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? CompanyName { get; set; }

    public int IdentificationTypeId { get; set; }

    public string IdentificationNumber { get; set; } = null!;

    public int? ExperienceYears { get; set; }

    public bool Freelance { get; set; }

    public virtual IdentificationType IdentificationType { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual ICollection<Referral> Referrals { get; } = new List<Referral>();

    public virtual ICollection<SellerSocialMedium> SellerSocialMedia { get; } = new List<SellerSocialMedium>();

    public virtual ICollection<Service> Services { get; } = new List<Service>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<VerificationRequest> VerificationRequests { get; } = new List<VerificationRequest>();
}
