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
        var (success, errors) = await authService.RegisterStudent(user);

        if (!success)
        {
            return BadRequest(errors);
        }

        return Ok(user);
    }

    [HttpPost("register-instructor")]
    public async Task<IActionResult> RegisterInstructor(RegisterUserDTO user)
    {
        var (success, errors) = await authService.RegisterInstructor(user);

        if (!success)
        {
            return BadRequest(errors);
        }

        return Ok(user);
    }
}