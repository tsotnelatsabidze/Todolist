using Microsoft.EntityFrameworkCore;
using TodoListApp.Services;
using TodoListApp.Services.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITodoListService, TodoListDatabaseService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoListDbContext>(opionts =>
{
    _ = opionts.UseSqlServer(builder.Configuration["ConnectionStrings:TodoListConnection"]);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
