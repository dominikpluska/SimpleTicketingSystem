using DataAccess.Repository.Interface;
using DataAccess.Repository;
using LogAPI.Data;
using Microsoft.EntityFrameworkCore;
using DataAccess.StaticData;
using LogAPI.UnitOfWork;
using Microsoft.Extensions.Configuration;
using GlobalServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(Configuration.GetDatabaseString("SimpleTicketingSystem_Logs"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
SeedDataBase();
app.MapControllers();

app.Run();



void SeedDataBase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }

        dbContext.Database.CloseConnection();
    }
}