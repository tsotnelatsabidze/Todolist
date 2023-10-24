using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Major Code Smell",
    "S3928:Parameter names used into ArgumentException constructors should match an existing one",
    Justification = "The parameter name is intentionally different for clarity.",
    Scope = "member",
    Target = "~M:TodoListApp.Services.Database.Services.TodoListDatabaseService.DeleteTodoList(System.Int32)")]

[assembly: SuppressMessage(
    "Major Code Smell",
    "S3928:Parameter names used into ArgumentException constructors should match an existing one",
    Justification = "The parameter name is intentionally different for clarity.",
    Scope = "member",
    Target = "~M:TodoListApp.Services.Database.Services.TodoListDatabaseService.GetTodoListById(System.Int32)~TodoListApp.Services.Models.TodoList")]

[assembly: SuppressMessage(
    "Major Code Smell",
    "S3928:Parameter names used into ArgumentException constructors should match an existing one",
    Justification = "The parameter name is intentionally different for clarity.",
    Scope = "member",
    Target = "~M:TodoListApp.Services.Database.Services.TodoListDatabaseService.UpdateTodoList(System.Int32,TodoListApp.Services.Models.TodoList)~TodoListApp.Services.Models.TodoList")]

[assembly: SuppressMessage(
    "Critical Code Smell",
    "S927:Parameter names should match base declaration and other partial definitions",
    Justification = "The parameter names are intentionally different for clarity.",
    Scope = "member",
    Target = "~M:TodoListApp.Services.Database.Services.TodoTaskDatabaseService.UpdateTodoTask(System.Int32,TodoListApp.Services.Models.TodoTask)~TodoListApp.Services.Models.TodoTask")]

[assembly: SuppressMessage(
    "Major Bug",
    "S2259:Null pointers should not be dereferenced",
    Justification = "Null pointer dereference is handled appropriately in the code.",
    Scope = "member",
    Target = "~M:TodoListApp.Services.Database.Services.TodoTaskDatabaseService.GetTodoTask(System.Int32)~TodoListApp.Services.Models.TodoTask")]
