using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorBookList.Migrations
{
    public partial class _alter_Book_alert_CoverImage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Cover",
                table: "Books",
                type: "bytea",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Books");
        }
    }
}
