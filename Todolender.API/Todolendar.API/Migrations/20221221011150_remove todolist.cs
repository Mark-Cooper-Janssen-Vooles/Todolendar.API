using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolendar.API.Migrations
{
    /// <inheritdoc />
    public partial class removetodolist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_TodosList_TodoListId",
                table: "Todos");

            migrationBuilder.DropTable(
                name: "TodosList");

            migrationBuilder.DropIndex(
                name: "IX_Todos_TodoListId",
                table: "Todos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodosList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodosList", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodoListId",
                table: "Todos",
                column: "TodoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_TodosList_TodoListId",
                table: "Todos",
                column: "TodoListId",
                principalTable: "TodosList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
