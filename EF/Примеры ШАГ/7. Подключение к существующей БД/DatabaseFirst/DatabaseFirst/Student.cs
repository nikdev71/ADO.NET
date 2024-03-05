
namespace DatabaseFirst;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int? Age { get; set; }

    public double? PointAverage { get; set; }

    public int? AcademyGroupId { get; set; }

    public AcademyGroup AcademyGroup { get; set; }
}
