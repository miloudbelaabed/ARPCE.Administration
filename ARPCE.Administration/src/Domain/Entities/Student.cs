
namespace ARPCE.Administration.Domain.Entities;
public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}

