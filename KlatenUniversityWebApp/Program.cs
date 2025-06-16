using FluentValidation;
using KlatenUniversityWebApp.Data;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Repositories;
using KlatenUniversityWebApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlite("Data Source=KlatenUniversity.db"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IValidator<Student>, StudentValidator>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register Specific Repositories
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();

// Register Services
builder.Services.AddScoped<IStudentsServices, StudentsServices>();

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

app.Run();
