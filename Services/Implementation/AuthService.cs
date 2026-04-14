using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Data.Entities;
using CollegeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CollegeManagementSystem.Services.Implementation;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    RoleManager<IdentityRole<Guid>> roleManager
) :
    IAuthService
{
    public async Task<(bool Success, List<string> Errors)> RegisterStudent(RegisterUserDTO data)
    {
        var user = new User
        {
            UserName = (data.FirstName + data.LastName).ToLower(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Phone = data.Phone
        };

        var result = await userManager.CreateAsync(user, data.Password);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(x => x.Description).ToList());
        }

        var addRoleStudent = await userManager.AddToRoleAsync(user, "Student");
        if (!addRoleStudent.Succeeded)
        {
            return (false, addRoleStudent.Errors.Select(x => x.Description).ToList());
        }

        {
            return (true, new List<string>());
        }
    }

    public async Task<(bool Success, List<string> Errors)> RegisterInstructor(RegisterUserDTO data)
    {
        var user = new User
        {
            UserName = (data.FirstName + data.LastName).ToLower(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Phone = data.Phone
        };
        var result = await userManager.CreateAsync(user, data.Password);
        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(x => x.Description).ToList());
        }

        var addRoleInstructor = await userManager.AddToRoleAsync(user, "Instructor");
        if (!addRoleInstructor.Succeeded)
        {
            return (false, addRoleInstructor.Errors.Select(x => x.Description).ToList());
        }

        return (true, new List<string>());
    }
}