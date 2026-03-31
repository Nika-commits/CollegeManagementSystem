using CollegeManagementSystem.Data;
using CollegeManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetModules()
    {
        var modules = await db.Modules.ToListAsync();
        return Ok(modules);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetModuleById(int id)
    {
        var module = await db.Modules.Where(m => m.Id == id).FirstOrDefaultAsync();
        return Ok(module);
    }

    [HttpPost]
    public async Task<IActionResult> CreateModule(Module module)
    {
        db.Modules.Add(module);
        await db.SaveChangesAsync();
        return Ok(module);
    }


}