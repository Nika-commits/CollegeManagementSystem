using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Data.Entities;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController(AppDbContext db) : Controller
{

    [HttpGet]
    public async Task<IActionResult> ListAllEnrollments()
    {
        var enrollments = await db.Enrollments.ToListAsync();
        return Ok(enrollments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEnrollment(Enrollment enrollment)
    {
        db.Enrollments.Add(enrollment);
        await db.SaveChangesAsync();
        return Ok();
    }

    // [HttpDelete("{studentId}/{courseId}")]
    // public async Task<IActionResult> DeleteEnrollment(int studentId, int courseId)
    // {
    //
    //     var deleted = await db.Enrollments
    //         .Where(e => e.StudentId == studentId && e.CourseId == courseId)
    //         .ExecuteDeleteAsync();
    //
    //     if (deleted == 0)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok();
    // }

    [HttpPost]
    public async Task<IActionResult> BulkPostEnrollment(List<Enrollment> enrollments)
    {
        await db.Enrollments.AddRangeAsync(enrollments);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("full-details")]
    public async Task<IActionResult> GetFullDetails()
    {
        var fullDetails = await db.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync();
        return Ok(fullDetails);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        var count = await db.Enrollments.CountAsync();
        return Ok(count);
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetEnrollmentsByDate(DateTime date)
    {
        var enrollments = await db.Enrollments.Where(e => e.EnrolledDate.Year == date.Year).ToListAsync();
        return Ok(enrollments);
    }
}