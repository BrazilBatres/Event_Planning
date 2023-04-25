using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventPlannerModels;

public partial class User
{
    public User()
    {
        this.Password = "password";
    }
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [DisplayName("Nombre")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "El apellido es requerido")]
    [DisplayName("Apellido")]
    public string LastName { get; set; } = null!;
    [DisplayName("Nombre de la empresa")]
    public string? CompanyName { get; set; }

    public bool MailVisible { get; set; }
    [Required(ErrorMessage = "El email es requerido")]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    public bool PhoneVisible { get; set; }

    public string? ContactPhone { get; set; }

    public bool IsCompany { get; set; }
    [Required(ErrorMessage = "La contraseña es requerida")]
    [DisplayName("Contraseña")]
    public string Password { get; set; } = null!;

    public virtual ICollection<Administrator> Administrators { get; } = new List<Administrator>();

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<Seller> Sellers { get; } = new List<Seller>();
}
