namespace WebApi.Startup;

public static class MapEndPoint
{
    public static WebApplication AppUsingPoints(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "DrasatHeathApi v1");
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "DrasatHeathApi v2");
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
        return app;
    }

}

