using CollegeManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Module> Modules { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<ModuleInstructor> ModuleInstructors { get; set; }

    public DbSet<Instructor> Instructors { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
    }
}