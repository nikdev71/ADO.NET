using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class Sgroup
{
    public int GrpPk { get; set; }

    public int? DepFk { get; set; }

    public decimal Num { get; set; }

    public decimal Kurs { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Rating { get; set; }

    public int? CurFk { get; set; }

    public virtual Teacher CurFkNavigation { get; set; }

    public virtual Department DepFkNavigation { get; set; }

    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}
