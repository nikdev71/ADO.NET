using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class Faculty
{
    public int FacPk { get; set; }

    public string Name { get; set; }

    public string Dean { get; set; }

    public string Building { get; set; }

    public decimal? Fund { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
