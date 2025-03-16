using MapZter.API.Triggers;
using MapZter.Contracts.Interfaces.Repository;
using MapZter.Contracts.Interfaces.Logger;
using MapZter.Logger;
using MapZter.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using System.Reflection;
using MapZter.Contracts.Interfaces.RepositoryProxy;
using MapZter.Entity.Models;

namespace MapZter.API.Extensions;

public class ServiceConfigure
{
    public readonly RepositoryManager _repositoryManager;
    public ServiceConfigure(RepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public void CreatePlace(Place inputEntity)
    {
        _repositoryManager.PlaceRepository.Create(inputEntity);
    }

    public void UpdatePlace(Place inputEntity)
    {
        // _repositoryManager.PlaceRepository.Update(inputEntity);
    }



    public static void ConfigureRepositoryStrategy()
    {
        //Entity : Place | Address | GeoPoint | GeoTag 
        //Create
        //Delete
        //Update
        //GetOne
        //GetSetById
        //GetSetByCondition

        // bool createPlaceStrategy = entity => 
        // {

        //     return true;
        // };

        var deleteStrategy = "";
        var updateStrategy = "";

        var getPlaceById = "";
        var getPlacesByIds = "";
        var getAddressById = "";
        var getAddressesByIds = "";
        var getGeoPointById = "";
        var getGeoPointsByIds = "";
        var getGeoTagById = "";
        var getGeoTagsByIds = ""; 
        // by id, by condition
    }
} 

public static class ServiceExtensions
{
    //Here we allow every request from any domain
	public static void ConfigureCors(this IServiceCollection services) =>
		services.AddCors(options => 
		{
            options.AddPolicy("CorsPolicy", builder =>
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
		});

	//Provides logger service
	public static void ConfigureLoggerService(this IServiceCollection services) =>
		services.AddTransient<ILoggerManager, LoggerManager>();

    public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration, DATABASE_TRIGGERS databaseType)
    {
        services.AddDbContext<RepositoryContext>(opts => {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            switch(databaseType)
            {
                case DATABASE_TRIGGERS.POSTGRESQL : 
                    opts.UseNpgsql(configuration.GetConnectionString("postgresConnection"), b => b.MigrationsAssembly(assemblyName));
                    break;
                case DATABASE_TRIGGERS.MSSQL :
                    opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly(assemblyName));
                    break;
                case DATABASE_TRIGGERS.IN_MEMORY :
                    opts.UseInMemoryDatabase("InMemory");
                    break;
            }
            });
    }

	public static void ConfigureRepositoryManager(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureResponseCaching(this IServiceCollection services) =>
        services.AddResponseCaching();

    public static void RegisterHostedService<T>(this IServiceCollection services, IEnumerable<T> backgroundServices) where T : IHostedService =>
        services.RegisterHostedService<T>(backgroundServices);
    
    public static void ConfigureApplication(this IServiceCollection services)
    {
        //register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        //register entity mapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies() );
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo{Title = "Company Employee API", Version="v1"});

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                    },
                    new List<string>()
                }
            });
        });
    }
}