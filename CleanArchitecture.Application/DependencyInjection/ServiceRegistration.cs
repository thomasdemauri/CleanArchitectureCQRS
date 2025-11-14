using CleanArchitecture.Application.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application.DependencyInjection
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(RequestResponseLogginBehaviour<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            return services;
        }

    }
}
