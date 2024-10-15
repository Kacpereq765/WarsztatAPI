using warsztat.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazor");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
