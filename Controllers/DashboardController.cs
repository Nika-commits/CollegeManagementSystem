using CollegeManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController(AppDbContext db) : Controller
{
    [HttpGet("dashboard")]
    public async Task<IActionResult> Summary()
    {
        var totalStudents = await db.Students.ToListAsync();
        var totalCourses = await db.Courses.ToListAsync();
        var totalModules = await db.Modules.ToListAsync();
        var totalEnrollments = await db.Enrollments.ToListAsync();

        return Ok(new
        {
            TotalStudents = totalStudents,
            TotalCourses = totalCourses,
            TotalModules = totalModules,
            TotalEnrollments = totalEnrollments
        });
    }
}