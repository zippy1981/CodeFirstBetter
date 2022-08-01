using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zippysoft.CodeFirst.DAL.Migrations
{
    public partial class UserDbaYahooAddresses : Migration
    {
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_YahooCom",
                table: "Users",
                column: "AlternateEmail")
                .Annotation("SqlServer:Include", new[] { "Id", "DisplayName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_YahooCom",
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
