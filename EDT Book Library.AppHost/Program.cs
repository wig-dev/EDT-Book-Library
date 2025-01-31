var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.EDT_Book_Library>("edt-book-library");

builder.Build().Run();
