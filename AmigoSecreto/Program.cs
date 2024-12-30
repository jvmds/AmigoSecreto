using System.Text.Json;
using System.Text.Json.Serialization;
using AmigoSecreto.Dtos;
using AmigoSecreto.Endpoints;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseArchitecture();
app.MapGroupEndpoints();
app.MapUserEndpoints();
app.MapUserGroupEndpoints();

app.Run();
