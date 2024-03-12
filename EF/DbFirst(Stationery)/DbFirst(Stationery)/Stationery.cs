using System;
using System.Collections.Generic;

namespace DbFirst_Stationery_;

public partial class Stationery
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Quantity { get; set; }

    public int Cost { get; set; }

    public int? TypeId { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual TypesOfStationery? Type { get; set; }
}
