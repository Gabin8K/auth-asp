using System.Security.Claims;
using Fullstack.Data;
using Fullstack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllers();
app.UseHttpsRedirection();
app.MapGet("/", (ClaimsPrincipal user) => $"{user.Identity?.Name}")
    .RequireAuthorization();

app.Run();