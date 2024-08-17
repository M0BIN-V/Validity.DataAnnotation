using Microsoft.Extensions.DependencyInjection;
using Validity.DataAnnotation.Options;

namespace Validity.DataAnnotation;

public static class DepenencyInjection
{
    public static IServiceCollection AddObjectValidator(this IServiceCollection services)
    {
        services.AddSingleton<IObjectValidator, ObjectValidator>();
        services.AddOptions();

        return services;
    }

    public static IServiceCollection AddObjectValidator(this IServiceCollection services, Action<ObjectValidatorOptions> setupAction)
    {
        services.AddSingleton<IObjectValidator, ObjectValidator>();
        services.Configure(setupAction);

        return services;
    }
}
