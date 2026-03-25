using CollegeManagementSystem.Data;
using CollegeManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModuleInstructorsController(AppDbContext db) : Controller
{
    [HttpPost]
    public async Task<IActionResult> AssignInstructorToModule(ModuleInstructor instructor)
    {
        await db.ModuleInstructors.AddAsync(instructor);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModuleInstructor(ModuleInstructor instructor)
    {
        var result = await db.ModuleInstructors.Where(mi => mi == instructor).ExecuteDeleteAsync();

        return Ok(result);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> BulkAddModuleInstructor(ModuleInstructor instructors)
    {
        await db.ModuleInstructors.AddRangeAsync(instructors);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("full-details")]
    public async Task<IActionResult> GetFullModuleInstructorDetails()
    {
        var instructors = await db.ModuleInstructors
            .Include(mi => mi.Module)
            .Include(mi => mi.Instructor)
            .ToListAsync();
        return Ok(instructors);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetModuleInstructorCount()
    {
        var count = await db.ModuleInstructors.CountAsync();
        return Ok(count);
    }
}