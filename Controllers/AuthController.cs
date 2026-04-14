using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterUserDTO user)
    {
        var (success, errors) = await authService.RegisterUser(user);

        if (!success)
        {
            return BadRequest(errors);
        }

        return Ok(user);
    }
}