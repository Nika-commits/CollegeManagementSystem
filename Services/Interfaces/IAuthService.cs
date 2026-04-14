using CollegeManagementSystem.Data.DTO.Request;

namespace CollegeManagementSystem.Services.Interfaces;

public interface IAuthService
{
    public Task<(bool Success, List<string> Errors)> RegisterUser(RegisterUserDTO registerUserDto);
}