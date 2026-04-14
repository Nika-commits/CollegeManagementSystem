using CollegeManagementSystem.Data.DTO.Request;

namespace CollegeManagementSystem.Services.Interfaces;

public interface IAuthService
{
    public Task<(bool Success, List<string> Errors)> RegisterStudent(RegisterUserDTO registerUserDto);
    public Task<(bool Success, List<string> Errors)> RegisterInstructor(RegisterUserDTO registerUserDto);
    public Task<(bool Success, List<string> Errors)> LoginUser(LoginUserDto loginUserDto);
}