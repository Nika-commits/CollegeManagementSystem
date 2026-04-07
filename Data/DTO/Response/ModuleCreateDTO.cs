using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Data.DTO.Response;

public class ModuleCreateDto
{
    [Required] public string Title { get; set; } = string.Empty;

    [Required] public string Description { get; set; } = string.Empty;

    [Required] [Range(1, 50)] public int Credits { get; set; }

    [Required] public int CourseId { get; set; }
}