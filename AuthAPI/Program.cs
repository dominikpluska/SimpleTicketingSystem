using AuthAPI.CookieGenerator;
using AuthAPI.Data;
using AuthAPI.JwtGenerator;
using AuthAPI.JwtGenerator.ICreateJwtToken;
using AuthAPI.Services;
using AuthAPI.UnitOfWork;
using AuthAPI.UnitOfWork.Interfaces;
using DataAccess.StaticData;
using GlobalServices.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(StaticData.ReturnConnectionString("SimpleTicketingSystem_UserData"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("Log", x => x.BaseAddress = new Uri("https://localhost:7201"));

builder.Services.AddScoped<IUnitOfWorkAuth, UnitOfWorkAuth>();
builder.Services.AddScoped<IUnitOfWorkUser, UnitOfWorkUser>();
builder.Services.AddScoped<IUnitOfWorkRole, UnitOfWorkRole>();
builder.Services.AddScoped<IUnitOfWorkGroup, UnitOfWorkGroup>();
builder.Services.AddScoped<ICreateJwtToken, CreateJwtToken>();
builder.Services.AddScoped<ICookieGenerator, CookieGenerator>();
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