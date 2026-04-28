using CollegeManagementSystem.Data;
using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Data.Entities;
using CollegeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CollegeManagementSystem.Services.Implementation;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    AppDbContext db
) :
    IAuthService
{
    public async Task<(bool Success, List<string> Errors)> RegisterStudent(RegisterUserDTO data)
    {
        var transaction = await db.Database.BeginTransactionAsync();
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
            await transaction.RollbackAsync();
            return (false, addRoleStudent.Errors.Select(x => x.Description).ToList());
        }

        await transaction.CommitAsync();
        return (true, []);
    }

    public async Task<(bool Success, List<string> Errors)> RegisterInstructor(RegisterUserDTO data)
    {
        var transaction = await db.Database.BeginTransactionAsync();
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
            await transaction.RollbackAsync();
            return (false, addRoleInstructor.Errors.Select(x => x.Description).ToList());
        }

        await transaction.CommitAsync();
        return (true, []);
    }

    public async Task<(bool Success, List<string> Errors)> LoginUser(LoginUserDto data)
    {
        var user = await userManager.FindByEmailAsync(data.Email);
        if (user == null)
        {
            return (false, ["Invalid Email or Password"]);
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, data.Password, false);

        if (!result.Succeeded)
        {
            return (false, ["Invalid Email or Password"]);
        }

        var roles = await userManager.GetRolesAsync(user);

        return (true, []);
    }
}