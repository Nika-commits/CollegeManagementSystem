using CollegeManagementSystem.Data;
using CollegeManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var allStudents = await db.Students.ToListAsync();
        return Ok(allStudents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await db.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
        return Ok(student);
    }

    [HttpGet("{id}/courses")]
    public async Task<IActionResult> GetCoursesEnrolledByStudent(int id)
    {
        var coursesEnrolled = await db.Enrollments
            .Where(e => e.StudentId == id)
            .Join(db.Courses, e => e.CourseId,
                c => c.Id,
                (e, c) => new
                {
                    c.Id,
                    c.Name,
                    c.Duration,

                    e.EnrolledDate
                }).ToListAsync();
        return Ok(coursesEnrolled);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(Student student)
    {
        db.Students.Add(student);
        await db.SaveChangesAsync();
        return Ok("Student Added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        if (id != student.Id)
        {
            return BadRequest();
        }

        var studentToUpdate = await db.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
        if (studentToUpdate == null)
        {
            return NotFound();
        }

        studentToUpdate.FirstName = student.FirstName;
        studentToUpdate.LastName = student.LastName;
        studentToUpdate.DateOfBirth = student.DateOfBirth;
        studentToUpdate.Phone = student.Phone;
        studentToUpdate.Email = student.Email;

        await db.SaveChangesAsync();
        return Ok("Student Updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var studentToDelete = await db.Students.Where(s => s.Id == id).FirstOrDefaultAsync();

        if (studentToDelete == null)
        {
            return NotFound();
        }

        db.Students.Remove(studentToDelete);
        await db.SaveChangesAsync();
        return Ok("Student Deleted");
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> AddStudentsInBulk(List<Student> students)
    {
        await db.Students.AddRangeAsync(students);
        await db.SaveChangesAsync();
        return Ok("Students Added Successfully");
    }

    [HttpGet("with-courses")]
    public async Task<IActionResult> GetStudentWithCourses()
    {
        var studentWithCourses = await db.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .ToListAsync();
        return Ok(studentWithCourses);
    }

    [HttpGet("count")]
    public async Task<IActionResult> TotalStudents(int id)
    {
        var totalStudents = await db.Students.CountAsync();
        return Ok(totalStudents);
    }

    [HttpGet("full-details")]
    public async Task<IActionResult> StudentsFullDetails()
    {
        var studentsWithFullDetails = await db.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ThenInclude(c => c.Modules).ToListAsync();

        return Ok(studentsWithFullDetails);
    }
}