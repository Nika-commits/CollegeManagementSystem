using CollegeManagementSystem.Data;
using CollegeManagementSystem.Data.DTO.Response;
using CollegeManagementSystem.Data.Entities;
using CollegeManagementSystem.Services.Interfaces;

namespace CollegeManagementSystem.Services.Implementation;

public class ModuleService(AppDbContext db) : IModuleService
{
    public async Task<string> AddModule(ModuleCreateDto moduleDto)
    {
        var module = new Module
        {
            Title = moduleDto.Title,
            Description = moduleDto.Description,
            Credits = moduleDto.Credits
        };


        db.Modules.Add(module);
        await db.SaveChangesAsync();
        return module.Title;
    }
}