using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zippysoft.CodeFirst.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserStateFilteredbyNY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "CHAR(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(25)",
                oldMaxLength: 25);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Users",
                type: "CHAR(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_State_NY",
                table: "Users",
                column: "State",
                filter: "[State] = 'NY'")
                .Annotation("SqlServer:Include", new[] { "Id", "DisplayName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_State_NY",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "CHAR(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(36)",
                oldMaxLength: 36);
        }
    }
}
