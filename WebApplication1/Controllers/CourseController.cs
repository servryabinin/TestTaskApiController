using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.CalassesDTO;
using WebApplication1.ClassesEntities;
using WebApplication1;

namespace CourseAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CoursesController : ControllerBase
  {
    private readonly CourseDbContext _context;

    public CoursesController(CourseDbContext context)
    {
      _context = context;
    }

    // GET /courses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
      var courses = await _context.Courses
          .Include(c => c.CourseStudents)
          .ThenInclude(cs => cs.Student)
          .Select(c => new CourseDto
          {
            Id = c.Id,
            Name = c.Name,
            Students = c.CourseStudents
                  .Select(cs => new StudentDto
                  {
                    Id = cs.Student.Id,
                    FullName = cs.Student.FullName
                  })
                  .ToList()
          })
          .ToListAsync();

      return Ok(courses);
    }

    // POST /courses
    [HttpPost]
    public async Task<ActionResult<CourseIdDto>> CreateCourse(CreateCourseDto createCourseDto)
    {
      var course = new Course
      {
        Id = Guid.NewGuid(),
        Name = createCourseDto.Name
      };

      _context.Courses.Add(course);
      await _context.SaveChangesAsync();

      return Ok(new CourseIdDto { Id = course.Id });
    }

    // POST /courses/{id}/students
    [HttpPost("{id:guid}/students")]
    public async Task<ActionResult<StudentIdDto>> AddStudentToCourse(Guid id, CreateStudentDto createStudentDto)
    {
      var course = await _context.Courses.FindAsync(id);
      if (course == null)
      {
        return NotFound();
      }

      var student = new Student
      {
        Id = Guid.NewGuid(),
        FullName = createStudentDto.FullName
      };

      _context.Students.Add(student);
      _context.CourseStudents.Add(new CourseStudent
      {
        CourseId = id,
        StudentId = student.Id
      });

      await _context.SaveChangesAsync();

      return Ok(new StudentIdDto { Id = student.Id });
    }

    // DELETE /courses/{id}
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
      var course = await _context.Courses.FindAsync(id);
      if (course == null)
      {
        return NotFound();
      }

      _context.Courses.Remove(course);
      await _context.SaveChangesAsync();

      return Ok(new { message = "Course deleted successfully" });
    }
  }
}