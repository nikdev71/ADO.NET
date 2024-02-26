namespace CodeFirst.EagerLoading
{
    public class AcademyGroup
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
