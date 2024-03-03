using System.Security.Claims;
using Fullstack.Data;
using Fullstack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication();
builder.Services.AddAuthentication();
builder.Services.AddAuthorizationBuilder();
builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(option => { option.Password.RequireLowercase = true; });

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("sqliteConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapIdentityApi<User>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.MapRazorPages();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();