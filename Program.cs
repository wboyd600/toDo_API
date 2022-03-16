using toDo_API;
using toDo_API.Models;
using toDo_API.Repositories;
using toDo_API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Call DI extension function
builder.ConfigureServices();

// Add services to the container.
// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
