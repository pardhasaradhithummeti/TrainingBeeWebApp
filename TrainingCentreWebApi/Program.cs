using Microsoft.EntityFrameworkCore;
using TrainingCentreWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var conStr = builder.Configuration.GetConnectionString("TrainingCentreDbConnection");
builder.Services.AddDbContext<TrainingCentreDbContext>(options=>options.UseSqlServer(conStr));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
