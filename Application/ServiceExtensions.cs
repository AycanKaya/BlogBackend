using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Interfaces;
using Application.Services;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Protocols;

public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
        
        services.AddTransient<IJWTService, JWTService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IAdminService, AdminServices>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPostService, PostService>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddTransient<ITagService, TagService>();
        services.AddTransient<IPostTagService , PostTagService>();  

    }
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            //c.IncludeXmlComments(string.Format(@"{0}\CleanArchitecture.WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "WebApi",
                Description = "This Api will be responsible for overall data distribution and authorization.",
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
        });
    }

}