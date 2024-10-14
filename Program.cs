using WarsztatAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<warsztatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("warsztatDb")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        builder => builder.WithOrigins("https://localhost:7096")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Tutaj tworzysz obiekt app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Wywo³anie UseCors przeniesione po zainicjalizowaniu app
app.UseCors("AllowBlazor");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
