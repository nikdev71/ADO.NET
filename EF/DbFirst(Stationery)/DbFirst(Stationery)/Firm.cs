using System;
using System.Collections.Generic;

namespace DbFirst_Stationery_;

public partial class Firm
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
