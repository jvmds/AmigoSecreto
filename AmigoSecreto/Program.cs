using AmigoSecreto.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseArchitecture();
//app.MapSecretSantaEndpoints();

app.Run();
