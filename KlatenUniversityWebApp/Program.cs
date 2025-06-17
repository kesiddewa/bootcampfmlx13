using FluentValidation;
using KlatenUniversityWebApp.Data;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Repositories;
using KlatenUniversityWebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using KlatenUniversityWebApp.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("KlatenUniversityWebAppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'KlatenUniversityWebAppIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlite("Data Source=KlatenUniversity.db"));

builder.Services.AddDbContext<KlatenUniversityWebAppIdentityDbContext>(options =>
    options.UseSqlite("Data Source=KlatenUniversity.db"));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<KlatenUniversityWebAppIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IValidator<Student>, StudentValidator>();
builder.Services.AddTransient<IValidator<Course>, CourseValidator>();

builder.Services.AddAutoMapper(typeof(Program));

// Register Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register Specific Repositories
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();

// Register Services
builder.Services.AddScoped<IStudentsServices, StudentsServices>();
builder.Services.AddScoped<ICoursesServices, CoursesServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
