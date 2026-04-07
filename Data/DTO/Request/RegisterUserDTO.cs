using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Data.DTO.Request;

public class RegisterUserDTO
{
    [Required] [MaxLength(50)] public string FirstName { get; set; } = string.Empty;

    [Required] [MaxLength(50)] public string LastName { get; set; } = string.Empty;

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [Required] [Range(10, 10)] public int? Phone { get; set; }
}