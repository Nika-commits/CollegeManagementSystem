using CollegeManagementSystem.Data.DTO.Response;

namespace CollegeManagementSystem.Services.Interfaces;

public interface IModuleService
{
    public string AddModule(ModuleCreateDto addModuleDto);
}