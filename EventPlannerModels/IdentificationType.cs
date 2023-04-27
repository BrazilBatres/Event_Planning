using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventPlannerModels;

public partial class IdentificationType
{
    public int Id { get; set; }
    [DisplayName("Tipo de Identificación")]
    [Required(ErrorMessage ="El tipo de identificación es requerido")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Seller> Sellers { get; } = new List<Seller>();
}
