using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CollegeManagementSystem.Data.Entities;

public class User : IdentityUser<Guid>
{
    [Required] [MaxLength(255)] public string FirstName { get; set; } = string.Empty;

    [Required] [MaxLength(255)] public string LastName { get; set; } = string.Empty;

    [Required] [MaxLength(10)] public string Phone { get; set; } = string.Empty;
}