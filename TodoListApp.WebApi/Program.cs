using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database;
using TodoListApp.Services.Database.Services;
using TodoListApp.Services.Interfaces;
using TodoListApp.Services.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITodoTaskService, TodoTaskDatabaseService>();
builder.Services.AddScoped<ITodoListService, TodoListDatabaseService>();
builder.Services.AddHttpClient<TodoListWebApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["TodoListWebApiService:BaseAddress"]);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoListDbContext>(options =>
{
    _ = options.UseSqlServer(builder.Configuration.GetConnectionString("TodoListConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
