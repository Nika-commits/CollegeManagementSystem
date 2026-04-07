using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeedingv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1eac6b18-9381-47a6-a273-f3e52a0396db"), "b2c3d4e5-f6a7-8901-bcde-f12345678901", "Student", "STUDENT" },
                    { new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"), "a1b2c3d4-e5f6-7890-abcd-ef1234567890", "Admin", "ADMIN" },
                    { new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"), "c3d4e5f6-a7b8-9012-cdef-123456789012", "Staff", "STAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1eac6b18-9381-47a6-a273-f3e52a0396db"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"));

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");
        }
    }
}
