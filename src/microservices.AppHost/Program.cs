var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Test_API>("test-api");

builder.Build().Run();
