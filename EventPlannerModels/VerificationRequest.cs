using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EventPlannerModels;

public partial class VerificationRequest
{
    public int Id { get; set; }

    public int SellerId { get; set; }

    public int? AdminId { get; set; }
    [DisplayName("Descripción")]
    public string? Description { get; set; }
    [DisplayName("Comentarios del Administrador")]
    public string? AdminComments { get; set; }

    public int StatusId { get; set; }
    [DisplayName("Fecha de actualización")]
    public DateTime TransacDate { get; set; }

    public virtual User? Admin { get; set; }

    public virtual Seller? Seller { get; set; }

    public virtual VerificationStatus? Status { get; set; }
}
