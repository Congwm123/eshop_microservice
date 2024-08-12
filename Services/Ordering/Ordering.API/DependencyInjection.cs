using BuildingBlocks.Exceptions.Handler;
using Carter;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        return services;
    }

    public static WebApplication UseApiApplication(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(opt => { });
        return app;
    }
}
