using System.Collections.Generic;

namespace DatabaseFirst;

public partial class AcademyGroup
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
