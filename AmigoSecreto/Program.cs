using AmigoSecreto.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();
app.UseStatusCodePages(async statusCodeContext 
                => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
                                .ExecuteAsync(statusCodeContext.HttpContext));
app.UseArchitecture();
app.MapGroupEndpoints();
app.MapUserEndpoints();
app.MapUserGroupEndpoints();

app.Run();
