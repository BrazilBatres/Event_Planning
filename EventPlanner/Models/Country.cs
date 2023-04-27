using System;
using System.Collections.Generic;

namespace EventPlanner.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
