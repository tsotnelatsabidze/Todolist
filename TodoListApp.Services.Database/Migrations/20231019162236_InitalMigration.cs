using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "TodoList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfTasks = table.Column<int>(type: "int", nullable: false),
                    IsShared = table.Column<bool>(type: "bit", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_TodoList", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "TodoTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_TodoTasks", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "todo_list",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_todo_list", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_todo_list_TodoList_Id",
                        column: x => x.Id,
                        principalTable: "TodoList",
                        principalColumn: "Id");
                });

            _ = migrationBuilder.CreateTable(
                name: "TodoTaskTodoLists",
                columns: table => new
                {
                    TodoTaskId = table.Column<int>(type: "int", nullable: false),
                    TodoListId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_TodoTaskTodoLists", x => new { x.TodoTaskId, x.TodoListId });
                    _ = table.ForeignKey(
                        name: "FK_TodoTaskTodoLists_TodoList_TodoListId",
                        column: x => x.TodoListId,
                        principalTable: "TodoList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    _ = table.ForeignKey(
                        name: "FK_TodoTaskTodoLists_TodoTasks_TodoTaskId",
                        column: x => x.TodoTaskId,
                        principalTable: "TodoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_TodoTaskTodoLists_TodoListId",
                table: "TodoTaskTodoLists",
                column: "TodoListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "todo_list");

            _ = migrationBuilder.DropTable(
                name: "TodoTaskTodoLists");

            _ = migrationBuilder.DropTable(
                name: "TodoList");

            _ = migrationBuilder.DropTable(
                name: "TodoTasks");
        }
    }
}
