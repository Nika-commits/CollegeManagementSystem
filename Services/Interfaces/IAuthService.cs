using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Data.DTO.Response;

namespace CollegeManagementSystem.Services.Interfaces;

public interface IAuthService
{
    public Task<RegistrationResponse> RegisterStudent(RegisterUserDTO registerUserDto);
    public Task<RegistrationResponse> RegisterInstructor(RegisterUserDTO registerUserDto);
    public Task<(bool Success, List<string> Errors)> LoginUser(LoginUserDto loginUserDto);
}