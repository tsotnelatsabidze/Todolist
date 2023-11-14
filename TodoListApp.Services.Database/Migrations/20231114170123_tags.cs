using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    public partial class tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CreatDate",
                table: "Comments",
                newName: "CreateDate");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Comments",
                newName: "CreatDate");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
