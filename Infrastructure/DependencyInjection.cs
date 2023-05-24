using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Users.Commands.AuthenticateUser;
using Application.Users.Commands.CreateUser;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddScoped<CreateUserCommandHandler>();
        services.AddScoped<AuthenticateUserCommandHandler>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}