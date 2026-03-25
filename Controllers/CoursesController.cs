using CollegeManagementSystem.Data;
using CollegeManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(AppDbContext db) : ControllerBase
{
//     private AppDbContext _db;
//
//     public CoursesController(AppDbContext db)
//     {
//         this._db = db;
//     }

    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var allCourses = await db.Courses.ToListAsync();
        var count = allCourses.Count;
        return Ok(new { count, allCourses });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseWithModules(int id)
    {
        var course = await db.Courses.Where(c => c.Id == id).Select(c => new
        {
            c.Id,
            c.Name,
            c.Duration,
            c.Modules
        }).FirstOrDefaultAsync();
        return Ok(course);
    }

    [HttpGet("{id}/modules")]
    public async Task<IActionResult> GetCourseModules(int id)
    {
        var modules = await db.Courses.Where(c => c.Id == id).Select(c => new { c.Id, c.Name, c.Modules })
            .ToListAsync();
        // var modules = await db.Modules.Where(m => m.CourseId == id);
        return Ok(modules);
    }

    [HttpGet("{id}/students")]
    public async Task<IActionResult> GetCourseStudents(int id)
    {
        var students = await db.Enrollments.Where(e => e.CourseId == id)
            .Include(e => e.Student)
            .Select(e => e.Student)
            .ToListAsync();
        return Ok(students);
    }

    [HttpPost]
    public IActionResult AddCourse(Course course)
    {
        db.Courses.Add(course);
        db.SaveChanges();
        return Ok("Course Added");
    }

    [HttpPost("{id}/modules")]
    public async Task<IActionResult> AddModuleToExisingCourse(int id, Module module)
    {
        var course = await db.Courses
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        course.Modules.Add(module);
        await db.SaveChangesAsync();

        return Ok("Module Added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, Course course)
    {
        var courseToUpdate = await db.Courses
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (courseToUpdate == null)
        {
            return NotFound();
        }

        courseToUpdate.Name = course.Name;
        courseToUpdate.Duration = course.Duration;

        course.Modules.Clear();

        foreach (var module in courseToUpdate.Modules) course.Modules.Add(module);

        await db.SaveChangesAsync();
        return Ok("Course Updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await db.Courses
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        await db.SaveChangesAsync();
        return Ok("Course Deleted");
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> BulkAddCourse(List<Course> courses)
    {
        await db.Courses.AddRangeAsync(courses);
        await db.SaveChangesAsync();
        return Ok("Course Added");
    }

    [HttpGet("with-details")]
    public async Task<IActionResult> GetCoursesWithDetails()
    {
        var coursesWithDetails = await db.Courses
            .Include(c => c.Modules)
            .ThenInclude(m => m.ModuleInstructors)
            .ToListAsync();

        return Ok(coursesWithDetails);
    }

    [HttpGet("counts")]
    public async Task<IActionResult> GetCounts()
    {
        var counts = await db.Courses.CountAsync();
        return Ok(counts);
    }

    [HttpGet("total-credits")]
    public async Task<IActionResult> GetTotalCredits()
    {
        var totalCredit = await db.Courses
            .Select(c => new
            {
                CourseName = c.Name,
                TotalCredits = c.Modules.Sum(m => m.Credits)
            }).ToListAsync();

        return Ok(totalCredit);
    }

}