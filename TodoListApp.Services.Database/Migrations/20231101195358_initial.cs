using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Tags", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "TodoTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TodoListId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_TodoTasks", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_TodoTasks_TodoLists_TodoListId",
                        column: x => x.TodoListId,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    CreatDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TodoTaskId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_Comments", x => x.Id);
                    _ = table.ForeignKey(name: "FK_Comments_TodoTasks_TodoTaskId", column: x => x.TodoTaskId, principalTable: "TodoTasks", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateTable(
                name: "TagEntityTodoTaskEntity",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    TodoTasksId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_TagEntityTodoTaskEntity", x => new { x.TagsId, x.TodoTasksId });
                    _ = table.ForeignKey(name: "FK_TagEntityTodoTaskEntity_Tags_TagsId", column: x => x.TagsId, principalTable: "Tags", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(name: "FK_TagEntityTodoTaskEntity_TodoTasks_TodoTasksId", column: x => x.TodoTasksId, principalTable: "TodoTasks", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_Comments_TodoTaskId",
                table: "Comments",
                column: "TodoTaskId");

            _ = migrationBuilder.CreateIndex(name: "IX_TagEntityTodoTaskEntity_TodoTasksId", table: "TagEntityTodoTaskEntity", column: "TodoTasksId");

            _ = migrationBuilder.CreateIndex(name: "IX_TodoTasks_TodoListId", table: "TodoTasks", column: "TodoListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "Comments");

            _ = migrationBuilder.DropTable(name: "TagEntityTodoTaskEntity");

            _ = migrationBuilder.DropTable(name: "Tags");

            _ = migrationBuilder.DropTable(name: "TodoTasks");

            _ = migrationBuilder.DropTable(name: "TodoLists");
        }
    }
}
