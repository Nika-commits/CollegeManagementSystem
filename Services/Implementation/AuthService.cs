using CollegeManagementSystem.Data;
using CollegeManagementSystem.Data.DTO.Request;
using CollegeManagementSystem.Data.DTO.Response;
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

    public async Task<RegistrationResponse> RegisterStudent(RegisterUserDTO data)
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
            return new RegistrationResponse
            {
                Success = false,
                Errors = result.Errors.Select(x => x.Description).ToList()
            };
        }

        var addRoleStudent = await userManager.AddToRoleAsync(user, "Student");
        if (!addRoleStudent.Succeeded)
        {
            await transaction.RollbackAsync();
            return new RegistrationResponse
            {
                Success = false,
                Errors = addRoleStudent.Errors.Select(x => x.Description).ToList()
            };
        }

        await transaction.CommitAsync();
        return new RegistrationResponse
        {
            Success = true,
            Errors = []
        };
    }

    public async Task<RegistrationResponse> RegisterInstructor(RegisterUserDTO data)
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
            return new RegistrationResponse
            {
                Success = false,
                Errors = result.Errors.Select(x => x.Description).ToList()
            };
        }

        var addRoleInstructor = await userManager.AddToRoleAsync(user, "Instructor");
        if (!addRoleInstructor.Succeeded)
        {
            await transaction.RollbackAsync();

            return new RegistrationResponse
            {
                Success = false,
                Errors = result.Errors.Select(x => x.Description).ToList()
            };
        }

        await transaction.CommitAsync();
        return new RegistrationResponse
        {
            Success = true,
            Errors = []
        };
    }

    public async Task<LoginResponse> LoginUser(LoginUserDto data)
    {
        var user = await userManager.FindByEmailAsync(data.Email);
        if (user == null)
        {
            return LoginResponse.Fail("Invalid Email or Password");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, data.Password, true);

        if (!result.Succeeded)
        {
            return LoginResponse.Fail("Invalid Email Or Password");
        }

        var roles = await userManager.GetRolesAsync(user);
        return LoginResponse.Ok("Token");
    }
}