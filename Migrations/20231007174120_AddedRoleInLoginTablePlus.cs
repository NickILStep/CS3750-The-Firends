using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment1v3.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleInLoginTablePlus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Login",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Login");
        }
    }
}
