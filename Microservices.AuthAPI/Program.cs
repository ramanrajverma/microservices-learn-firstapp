using Microservices.AuthAPI.Models;
using Microservices.AuthAPI.Service;
using Microservices.AuthAPI.Service.IService;
using Microservices.Services.AuthAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon"));

});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions")); //Configure the JWT options from appsettings.json
//Below configuring the identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()   //Default user was IdentityUser earlier, now I changed the Default use now is ApplicationUser
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IAuthService, AuthService>(); //Registering the AuthService
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); //Registering the JwtTokenGenerator

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Ensure authentication middleware is added
app.UseAuthorization();

app.MapControllers();
ApplyMigrations();
app.Run();

// To apply any pending migrations on running the application
void ApplyMigrations()
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while migrating the database. Error: {ex.Message}");
    }
}

