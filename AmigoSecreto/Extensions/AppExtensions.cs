using AmigoSecreto.Dtos;

namespace AmigoSecreto.Extensions;

public static class AppExtensions
{
    public static WebApplication UseArchitecture(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";
                await context.Response.WriteAsJsonAsync(ErrorDto.CreatedError500);
            });
        });

        app.UseStatusCodePages(async statusCodeContext
                => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
                                .ExecuteAsync(statusCodeContext.HttpContext));

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        return app;
    }
}