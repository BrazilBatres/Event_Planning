using System;
using System.Collections.Generic;

namespace EventPlannerModels;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<State> States { get; } = new List<State>();
}
