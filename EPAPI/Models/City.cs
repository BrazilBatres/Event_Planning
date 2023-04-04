﻿using System;
using System.Collections.Generic;

namespace EPAPI.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int StateId { get; set; }

    public virtual State State { get; set; } = null!;
}
