using CollegeManagementSystem.Data.DTO.Response;
using CollegeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController(IModuleService moduleService) : ControllerBase
{
    // [HttpGet]
    // public async Task<IActionResult> GetModules()
    // {
    //     var modules = await db.Modules.ToListAsync();
    //     return Ok(modules);
    // }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetModuleById(int id)
    // {
    //     var module = await db.Modules.Where(m => m.Id == id).FirstOrDefaultAsync();
    //     return Ok(module);
    // }

    [HttpPost]
    public async Task<IActionResult> AddModule(ModuleCreateDto moduleDto)
    {
        var response = await moduleService.AddModule(moduleDto);
        return Ok(response);
    }

    // [HttpGet("{id}/instructors")]
    // public async Task<IActionResult> GetInstructorByModule(int id)
    // {
    //     var instructors = await db.Modules
    //         .Include(m => m.ModuleInstructors)
    //         .ThenInclude(mi => mi.Instructor)
    //         .ToListAsync();
    //     return Ok(instructors);
    // }
}