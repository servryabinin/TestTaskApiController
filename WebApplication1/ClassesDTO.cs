namespace WebApplication1.CalassesDTO
{
  public class CourseDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<StudentDto> Students { get; set; }
  }

  public class StudentDto
  {
    public Guid Id { get; set; }
    public string FullName { get; set; }
  }

  public class CreateCourseDto
  {
    public string Name { get; set; }
  }

  public class CourseIdDto
  {
    public Guid Id { get; set; }
  }

  public class CreateStudentDto
  {
    public string FullName { get; set; }
  }

  public class StudentIdDto
  {
    public Guid Id { get; set; }
  }
}
