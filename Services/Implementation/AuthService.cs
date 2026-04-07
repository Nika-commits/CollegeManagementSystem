using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Data.Entities;
using CollegeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CollegeManagementSystem.Services.Implementation;

public class AuthService(UserManager<User> userManager) : IAuthService
{
    public async Task<string> RegisterUser(RegisterUserDTO registerUserDto)
    {
        var user = new User
        {
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            Email = registerUserDto.Email,
            Phone = registerUserDto.Phone
        };
        var registerResult = await userManager.CreateAsync(user);

        if (!registerResult.Succeeded)
        {
            return registerResult.ToString();
        }

        return user.Id.ToString();
    }
}