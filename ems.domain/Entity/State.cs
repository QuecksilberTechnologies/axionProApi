﻿using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class State
{
    public int Id { get; set; }

    public string StateName { get; set; } = null!;

    public int CountryId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country Country { get; set; } = null!;
}
