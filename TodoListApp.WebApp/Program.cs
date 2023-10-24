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
_ = app.UseAuthorization();

_ = app.UseEndpoints(endpoints => { _ = endpoints.MapControllerRoute(name: "TodoTaskEdit", pattern: "TodoTask/Edit/{taskId}", defaults: new { controller = "TodoTask", action = "Edit" }); });

_ = app.UseAuthentication();

_ = app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
_ = app.MapRazorPages();

app.Run();
