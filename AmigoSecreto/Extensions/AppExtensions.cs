namespace AmigoSecreto.Extensions;

public static class AppExtensions
{
    public static WebApplication UseArchitecture(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        return app;
    }
}