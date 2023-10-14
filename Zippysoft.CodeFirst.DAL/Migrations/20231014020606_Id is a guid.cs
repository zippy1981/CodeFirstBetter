using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zippysoft.CodeFirst.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Idisaguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uniqueidentifier",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(36)",
                oldMaxLength: 36);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "CHAR(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 36);
        }
    }
}
