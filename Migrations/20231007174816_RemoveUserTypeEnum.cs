using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment1v3.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Login",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
