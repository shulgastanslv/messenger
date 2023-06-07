using Application.Common.Abstractions;
using Application.Common.Interfaces;
using Application.Messages.Commands.SaveMessageCommand;
using Application.Users.Commands.UserAuthentication;
using Application.Users.Commands.UserRegistration;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using FluentValidation;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddSingleton(configuration.GetSection("FilePath:MessageFilePath").Value ?? "");

        services.AddTransient<IMessageRepository, MessageRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IChatRepository, ChatRepository>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IJwtProvider, JwtProvider>();

        services.AddScoped<UserRegistrationCommandHandler>();
        services.AddScoped<UserAuthenticationCommandHandler>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}