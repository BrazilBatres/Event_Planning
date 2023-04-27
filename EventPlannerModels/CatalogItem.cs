using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventPlannerModels;

public partial class CatalogItem
{
    public int Id { get; set; }

    public int SellerId { get; set; }
    [DisplayName("Nombre del producto")]
    [Required(ErrorMessage ="El nombre del producto es requerido")]
    public string ItemName { get; set; } = null!;
    [DisplayName("Descripción del producto")]
    [Required(ErrorMessage = "La descripción del producto es requerida")]
    public string ItemDescription { get; set; } = null!;
    [DisplayName("Precio del producto")]
    public decimal ItemPrice { get; set; }

    public int ItemCategoryId { get; set; }
    [DisplayName("¿Es un servicio?")]
    public sbyte IsService { get; set; }

    public virtual ICollection<EventItem> EventItems { get; set; } = new List<EventItem>();

    public virtual ItemCategory? ItemCategory { get; set; }

    public virtual User? Seller { get; set; }
}
