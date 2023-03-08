using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolendar.API.Migrations
{
    /// <inheritdoc />
    public partial class changetodlistidtouserid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TodoListId",
                table: "Todos",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Todos",
                newName: "TodoListId");
        }
    }
}
