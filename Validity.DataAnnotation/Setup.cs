using Microsoft.Extensions.DependencyInjection;
using Validity.DataAnnotation.Options;

namespace Validity.DataAnnotation;

public static class Setup
{
    public static IServiceCollection AddObjectValidator(this IServiceCollection services)
    {
        services.AddSingleton<ObjectValidator>();
        services.AddOptions();

        return services;
    }

    public static IServiceCollection AddObjectValidator(this IServiceCollection services, Action<ObjectValidatorOptions> setupAction)
    {
        services.AddSingleton<ObjectValidator>();

        services.Configure(setupAction);

        return services;
    }
}
