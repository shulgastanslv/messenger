using System.Reflection;
using Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
                var assembly = typeof(DependencyInjection).Assembly;

                services.AddMediatR(configuration =>
                {
                    configuration.RegisterServicesFromAssembly(assembly);
                    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                });

                        
                services.AddValidatorsFromAssembly(assembly);

                return services;
        }
}