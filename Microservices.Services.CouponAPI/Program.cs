using AutoMapper;
using Microservices.Services.CouponAPI;
using Microservices.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon"));

});

// Registering AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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
            if (_db.Database.GetPendingMigrations().Count()>0) 
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
