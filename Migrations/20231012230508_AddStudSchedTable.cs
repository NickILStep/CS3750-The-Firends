using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment1v3.Migrations
{
    /// <inheritdoc />
    public partial class AddStudSchedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudSched",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudSched", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudSched");
        }
    }
}
