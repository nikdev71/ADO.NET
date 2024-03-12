using System;
using System.Collections.Generic;

namespace DbFirst_Stationery_;

public partial class TypesOfStationery
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Stationery> Stationeries { get; set; } = new List<Stationery>();
}
