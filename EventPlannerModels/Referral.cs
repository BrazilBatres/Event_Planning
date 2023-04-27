using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EventPlannerModels;

public partial class Referral
{
    public int Id { get; set; }

    public int SellerId { get; set; }
    [Required(ErrorMessage = "El nombre es requerido")]
    [DisplayName("Nombre")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "El número de teléfono/celular es requerido")]
    [DisplayName("Teléfono")]
    public string Phone { get; set; } = null!;
    [Required(ErrorMessage = "El email es requerido")]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    public virtual Seller? Seller { get; set; }
}
