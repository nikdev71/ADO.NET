using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class Department
{
    public int DepPk { get; set; }

    public int FacFk { get; set; }

    public string Name { get; set; }

    public string Head { get; set; }

    public string Building { get; set; }

    public decimal? Fund { get; set; }

    public virtual Faculty FacFkNavigation { get; set; }

    public virtual ICollection<Sgroup> Sgroups { get; set; } = new List<Sgroup>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
