using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Data.DTO.Response;
using CollegeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register-student")]
    public async Task<IActionResult> RegisterStudent(RegisterUserDTO user)
    {
        var result = await authService.RegisterStudent(user);

        return !result.Success
            ? BadRequest(result.Errors)
            : Ok(new { Success = true, email = user.Email });
    }

    [HttpPost("register-instructor")]
    public async Task<IActionResult> RegisterInstructor(RegisterUserDTO user)
    {
        var result = await authService.RegisterInstructor(user);

        return !result.Success
            ? BadRequest(result.Errors)
            : Ok(new { Success = true, email = user.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserDto user)
    {
        var result = await authService.LoginUser(user);
        return !result.Success ? BadRequest(result.Error) : Ok(LoginResponse.Ok("token"));
    }
}