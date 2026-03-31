using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Data.DTO.Response;

public class ModuleCreateDto
{
    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [Required] [Range(1, 50)] public int Credits { get; set; }
}