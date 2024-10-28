using GlobalServices.Interface;
using MailAPI.Services;
using MailAPI.Services.IServices;
using TicketsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMailSender, MailSender>();

builder.Services.AddHttpClient("Log", x => x.BaseAddress = new Uri("https://localhost:7201"));
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

app.MapControllers();

app.Run();
