using System;
using System.Collections.Generic;

namespace VUZCodeFirst;

public partial class Post
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
