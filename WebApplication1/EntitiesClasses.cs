namespace WebApplication1.ClassesEntities
{
  public class Course
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<CourseStudent> CourseStudents { get; set; }
  }

  public class Student
  {
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public ICollection<CourseStudent> CourseStudents { get; set; }
  }

  public class CourseStudent
  {
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
  }
}
