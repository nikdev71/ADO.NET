using System;
using System.Collections.Generic;

namespace DbFirst_Stationery_;

public partial class Manager
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
