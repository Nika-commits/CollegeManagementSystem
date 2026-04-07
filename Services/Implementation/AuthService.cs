using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Services.Implementation;

public class AuthService : IAuthService
{
    public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDto)
    {
        return NoContent();
    }
}