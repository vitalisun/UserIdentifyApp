using IndentifyApp.BL.Services;
using IndentifyApp.DAL.Entities;
using IndentifyApp.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IndentifyApp.WebApp.SignalRHubs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSignalR();

// di
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder =>
{
    builder.WithOrigins("https://localhost:7189")
        .AllowAnyHeader()
        .WithMethods("GET", "POST")
        .AllowCredentials();
});

// routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<IdentifyProfileHub>("/identifyProfile");

app.Run();
