using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class Subject
{
    public int SbjPk { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}
