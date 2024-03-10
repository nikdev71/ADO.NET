using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class LectureType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}
