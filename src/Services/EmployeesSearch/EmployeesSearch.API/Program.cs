using EmployeesSearch.API.Consumers;
using EmployeesSearch.API.Data;
using MassTransit;
using MongoDB.Entities;
using BuildingBlocks.Messaging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddMessageBroker(builder.Configuration,Assembly.GetExecutingAssembly());

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
