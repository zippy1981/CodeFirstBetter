using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zippysoft.CodeFirst.DAL.Migrations
{
    public partial class UserDbaReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "CHAR(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AlternateEmail",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Manually added the drop and recreate of the primary key because we are altering the primary key, and its not smart enough to do that.
            migrationBuilder.DropPrimaryKey("PK_Users", "users");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "CHAR(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
            
            // the manual re-create 
            migrationBuilder.AddPrimaryKey("PK_Users", "Users", "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(21)",
                oldMaxLength: 21);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "AlternateEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            // Manually added the drop and recreate of the primary key because we are altering the primary key, and its not smart enough to do that.
            migrationBuilder.DropPrimaryKey("PK_Users", "users");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(36)",
                oldMaxLength: 26);

            // the manual re-create 
            migrationBuilder.AddPrimaryKey("PK_Users", "Users", "Id");
        }
    }
}
