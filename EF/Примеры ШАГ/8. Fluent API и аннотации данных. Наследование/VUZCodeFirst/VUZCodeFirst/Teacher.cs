using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class Teacher
{
    public int TchPk { get; set; }

    public int? DepFk { get; set; }

    public string Name { get; set; }

    public string Idcode { get; set; }

    public int IdPost { get; set; }

    public string Tel { get; set; }

    public decimal? Salary { get; set; }

    public decimal? Rise { get; set; }

    public DateTime? HireDate { get; set; }

    public int? Chief { get; set; }

    public virtual Teacher ChiefNavigation { get; set; }

    public virtual Department DepFkNavigation { get; set; }

    public virtual Post IdPostNavigation { get; set; }

    public virtual ICollection<Teacher> InverseChiefNavigation { get; set; } = new List<Teacher>();

    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();

    public virtual ICollection<Sgroup> Sgroups { get; set; } = new List<Sgroup>();
}
