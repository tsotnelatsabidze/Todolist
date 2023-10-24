using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database;
using TodoListApp.Services.Database.Services;
using TodoListApp.Services.Interfaces;
using TodoListApp.WebApi.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoListDbContext>(c =>
{
    var connectionString = builder.Configuration.GetConnectionString("ToDoListConnection");
    _ = c.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
});

builder.Services.AddScoped<ITodoListService, TodoListDatabaseService>();
builder.Services.AddScoped<ITodoTaskService, TodoTaskDatabaseService>();
builder.Services.AddAutoMapper(typeof(TodoListCreateProfile));
builder.Services.AddAutoMapper(typeof(TodoListUpdateProfile));
builder.Services.AddAutoMapper(typeof(TodoTaskProfile));
builder.Services.AddAutoMapper(typeof(TodoTaskUpdateProfile));

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Count().SetMaxTop(null));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
