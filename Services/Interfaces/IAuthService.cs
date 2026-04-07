using CollegeManagementSystem.Data.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Services.Interfaces;

public interface IAuthService
{
    public Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDto);
}