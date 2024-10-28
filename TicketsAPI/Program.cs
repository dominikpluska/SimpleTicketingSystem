using DataAccess;
using DataAccess.StaticData;
using GlobalServices.Interface;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;
using TicketsAPI.Services;
using TicketsAPI.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AppSettings.ConfigurationReader();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(StaticData.ReturnConnectionString("SimpleTicketingSystem_Tickets"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("Log", x => x.BaseAddress = new Uri("https://localhost:7201"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGlobalServices, Services>();

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
        dbContext.Database.Migrate();
        dbContext.Database.CloseConnection();
    }
}
