using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    public partial class Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(name: "Comment", table: "Comments");

            _ = migrationBuilder.DropColumn(name: "CreatorId", table: "Comments");

            _ = migrationBuilder.RenameColumn(name: "CreatDate", table: "Comments", newName: "CreateDate");

            _ = migrationBuilder.AddColumn<string>(name: "Creator", table: "Comments", type: "nvarchar(max)", nullable: true);

            _ = migrationBuilder.AddColumn<string>(name: "Text", table: "Comments", type: "nvarchar(max)", nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "Creator",
                table: "Comments");

            _ = migrationBuilder.DropColumn(name: "Text", table: "Comments");

            _ = migrationBuilder.RenameColumn(name: "CreateDate", table: "Comments", newName: "CreatDate");

            _ = migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);

            _ = migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);
        }
    }
}
