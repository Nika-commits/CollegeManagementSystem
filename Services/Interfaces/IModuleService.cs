using CollegeManagementSystem.Data.DTO.Response;

namespace CollegeManagementSystem.Services.Interfaces;

public interface IModuleService
{
    public Task<string> AddModule(ModuleCreateDto addModuleDto);
}