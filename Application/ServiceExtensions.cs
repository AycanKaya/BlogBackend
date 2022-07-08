using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Interfaces;
using Application.Services;

public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<IJWTService, JWTService>();
        services.AddTransient<IAccountService, AccountService>();

    }
}