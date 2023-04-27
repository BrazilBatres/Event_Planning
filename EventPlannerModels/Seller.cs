using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EventPlannerModels;

public partial class Seller
{
    public int Id { get; set; }

    public int UserId { get; set; }
    [DisplayName("Empresa")]
    public string? CompanyName { get; set; }

    
    public int IdentificationTypeId { get; set; }
    [Required(ErrorMessage = "El número de documento de identificación es requerido")]
    [DisplayName("No. de documento de identificación")]
    public string IdentificationNumber { get; set; } = null!;
    [DisplayName("Años de experiencia")]
    public int? ExperienceYears { get; set; }
    [DisplayName("Vendedor Autónomo")]
    public bool Freelance { get; set; }
    [DisplayName("Tipo de documento de identificación")]
    public virtual IdentificationType? IdentificationType { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual ICollection<Referral> Referrals { get; } = new List<Referral>();

    public virtual ICollection<SellerSocialMedium> SellerSocialMedia { get; } = new List<SellerSocialMedium>();

    public virtual ICollection<Service> Services { get; } = new List<Service>();

    public virtual User? User { get; set; }

    public virtual ICollection<VerificationRequest> VerificationRequests { get; } = new List<VerificationRequest>();
}
