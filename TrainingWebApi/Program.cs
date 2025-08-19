using Microsoft.EntityFrameworkCore;
using TrainingWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//fetch the connection string from the appsettings.json file
var connectionString = builder.Configuration.GetConnectionString("TrainingDbConnection");

//Dependency Injection
//configure the DbContext options for TrainingDbContext
builder.Services.AddDbContext<TrainingDbContext>(options => options.UseSqlServer(connectionString));

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
