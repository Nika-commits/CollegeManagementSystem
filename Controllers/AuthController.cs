using CollegeManagementSystem.Data.DTO.Request;
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

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { Success = true, User = user.Email });
    }

    [HttpPost("register-instructor")]
    public async Task<IActionResult> RegisterInstructor(RegisterUserDTO user)
    {
        var result = await authService.RegisterInstructor(user);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
            {
                Success = true,
                User = user.Email
            }
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserDto user)
    {
        var (success, errors) = await authService.LoginUser(user);

        if (!success)
        {
            return BadRequest(errors);
        }

        return Ok(new { Success = true, User = user.Email });
    }
}