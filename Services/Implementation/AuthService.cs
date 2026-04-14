using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Data.Entities;
using CollegeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CollegeManagementSystem.Services.Implementation;

public class AuthService(UserManager<User> userManager) : IAuthService
{
    public async Task<(bool Success, List<string> Errors)> RegisterUser(RegisterUserDTO data)
    {
        var user = new User
        {
            UserName = (data.FirstName + data.LastName).ToLower(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Phone = data.Phone
        };

        var result = await userManager.CreateAsync(user);

        return (
            result.Succeeded,
            result.Errors.Select(x => x.Description).ToList()
        );
    }
}