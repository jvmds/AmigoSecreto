using System.Text.Json;
using System.Text.Json.Serialization;
using AmigoSecreto.Dtos;
using AmigoSecreto.Endpoints;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();
app.UseStatusCodePages(async statusCodeContext 
                => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
                                .ExecuteAsync(statusCodeContext.HttpContext));
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(new ErrorDto());
    });
});
app.UseArchitecture();
app.MapGroupEndpoints();
app.MapUserEndpoints();
app.MapUserGroupEndpoints();

app.Run();
