using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeManagementSystem.Data.Entities;

[Table("Modules")]
public class Module
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(50)] public string Title { get; set; } = string.Empty;

    [Required] [MaxLength(500)] public string Description { get; set; } = string.Empty;

    [Required] public int Credits { get; set; }

    public Course? Course { get; set; }

    public List<ModuleInstructor> ModuleInstructors { get; set; } = [];
}