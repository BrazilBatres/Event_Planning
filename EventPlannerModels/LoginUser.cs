using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventPlannerModels;

public partial class LoginUser
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    
}
