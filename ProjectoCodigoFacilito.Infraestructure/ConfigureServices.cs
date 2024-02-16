using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectoCodigoFacilito.Domain.Entities;
using ProjectoCodigoFacilito.Domain.Repository;
using ProjectoCodigoFacilito.Infraestructure.Data;
using ProjectoCodigoFacilito.Infraestructure.Repository;

namespace ProjectoCodigoFacilito.Infraestructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfraestructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ProjectDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ProyectoCody") ??
                                 throw new InvalidOperationException(
                                     "Connection string 'ProjectDbContext' not found"))
            );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICharacterRepository, CharacterRepository>();
        services.AddTransient<IReferenceIdRepository, ReferenceIdRepository>();
        
        return services;
    }
}