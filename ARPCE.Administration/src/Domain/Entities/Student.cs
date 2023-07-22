
namespace ARPCE.Administration.Domain.Entities;
public class Student:BaseAuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<StudentCourse> StudentCourses { get; set; }
}

