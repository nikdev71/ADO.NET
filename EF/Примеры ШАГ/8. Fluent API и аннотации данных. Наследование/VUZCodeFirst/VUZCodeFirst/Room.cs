using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class Room
{
    public int RomPk { get; set; }

    public decimal Num { get; set; }

    public decimal? Floor { get; set; }

    public string Building { get; set; }

    public decimal? Seats { get; set; }

    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}
