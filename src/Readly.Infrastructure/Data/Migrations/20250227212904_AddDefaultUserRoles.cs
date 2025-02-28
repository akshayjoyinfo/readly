using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Readly.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "name", "created_at_utc", "created_by" },
                values: new object[,]
                {
                    { "Admin", DateTimeOffset.UtcNow, "SYSTEM" },
                    { "User", DateTimeOffset.UtcNow, "SYSTEM" },
                    { "Manager", DateTimeOffset.UtcNow, "SYSTEM" },
                    { "Guest", DateTimeOffset.UtcNow, "SYSTEM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE roles RESTART IDENTITY CASCADE;");
        }
    }
}
