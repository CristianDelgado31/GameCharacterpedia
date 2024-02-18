using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectoCodigoFacilito.Application.Common.Behaviours;

namespace ProjectoCodigoFacilito.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Add AutoMapper
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // Add FluentValidation validators
        services.AddMediatR(ctg =>
        {
            ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // validation behaviour
            ctg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>)); // ValidationBehaviour es un comportamiento que se ejecuta antes de cualquier request
        });

       
        return services;
    }
}