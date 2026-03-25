using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CollegeManagementSystem.Data.Entities;

[Table("Courses")]
public class Course
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; } = string.Empty;

    [Required] [Range(1, 10)] public int Duration { get; set; }

    [JsonIgnore] public List<Module> Modules { get; set; } = [];
}