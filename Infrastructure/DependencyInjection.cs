using Application.Common.Abstractions;
using Domain.Entities.Chats;
using Domain.Entities.Groups;
using Domain.Entities.Messages;
using Domain.Entities.UserGroups;
using Domain.Entities.Users;
using FluentValidation;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<IUserGroupRepository, UserGroupRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            options.UseLazyLoadingProxies();
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}