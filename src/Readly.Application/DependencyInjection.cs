using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Readly.Application.Common.Behaviours;
using Readly.Domain.Entities.UserAccess;

namespace Readly.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
       
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });


        

       
        return services;
    }
}
