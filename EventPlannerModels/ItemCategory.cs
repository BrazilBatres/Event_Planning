using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventPlannerModels;

public partial class ItemCategory
{
    
    public int Id { get; set; }
    [DisplayName("Categoría")]
    [Required(ErrorMessage = "El nombre de la categoría es requerido")]
    public string Category { get; set; } = null!;

    public virtual ICollection<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
}
