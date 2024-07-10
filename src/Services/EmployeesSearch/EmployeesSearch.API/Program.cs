using EmployeesSearch.API.Data;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

try
{
    await DbInitialiazer.InitDb(app);

}
catch (Exception e)
{
    Console.WriteLine("DbInitialiazer.Exception "+ e);
}
app.Run();
