using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolendar.API.Migrations
{
    /// <inheritdoc />
    public partial class useentityforscheduledTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RecurFrequencyType",
                table: "ScheduledTodo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecurFrequencyType",
                table: "ScheduledTodo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
