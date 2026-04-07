using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CollegeManagementSystem.Data.Entities;

public class User : IdentityUser<Guid>
{
    [Required] public string FirstName { get; set; } = string.Empty;

    [Required] public string LastName { get; set; } = string.Empty;
}