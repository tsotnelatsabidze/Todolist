using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Minor Code Smell",
    "S1075:URIs should not be hardcoded",
    Justification = "The URI is a constant in this context and does not need to be configurable.",
    Scope = "member",
    Target = "~M:TodoListApp.Services.WebApi.TodoListWebApiService.#ctor")]
