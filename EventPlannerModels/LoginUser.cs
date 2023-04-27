using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventPlannerModels;

public partial class LoginUser
{
    [Required(ErrorMessage = "El email es requerido")]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "La contraseña es requerida")]
    [DisplayName("Contraseña")]
    public string Password { get; set; } = null!;

    
}
