using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Messaging;
using FluentValidation;
using HealthChecks.UI.Client;
using MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    options.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    options.AddOpenBehavior(typeof(LoggingBehaviour<,>));

});
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddCarter();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("pgDbConnection"));
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//health check
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("pgDbConnection"));


builder.Services.AddMessageBroker(builder.Configuration);

var app = builder.Build();
try
{
    DbInitializer.InitDb(app);
}
catch (Exception)
{

}
//Confugure pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health-check",new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
