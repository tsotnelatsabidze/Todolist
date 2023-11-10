using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.WebApi;
using TodoListApp.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("IdentityConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(new TodoListWebApiService());
builder.Services.AddSingleton(new TodoTasksWebApiService());
builder.Services.AddSingleton(new CommentsWebApiService());
builder.Services.AddSingleton(new TagWebApiService());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseMigrationsEndPoint();
}
else
{
    _ = app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

_ = app.UseHttpsRedirection();
_ = app.UseStaticFiles();

_ = app.UseRouting();


_ = app.UseAuthentication();
_ = app.UseAuthorization();

_ = app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
_ = app.MapRazorPages();

app.Run();
