using MapZter.Contracts.Interfaces;
using MapZter.Logger;
using MapZter.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using System.Reflection;

namespace MapZter.API.Extensions;

public static class ServiceConfigure
{

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

    public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration, DATABASE_TYPE databaseType)
    {
        services.AddDbContext<RepostioryContext>(opts => {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            switch(databaseType)
            {
                case DATABASE_TYPE.POSTGRESQL : 
                    opts.UseNpgsql(configuration.GetConnectionString("postgresConnection"), b => b.MigrationsAssembly(assemblyName));
                    break;
                case DATABASE_TYPE.MSSQL :
                    opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly(assemblyName));
                    break;
                case DATABASE_TYPE.IN_MEMORY :
                    opts.UseInMemoryDatabase("InMemory");
                    break;
            }
            });
    }

	public static void ConfigureRepositoryManager(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepostioryManager>();

    public static void ConfigureResponseCaching(this IServiceCollection services) =>
        services.AddResponseCaching();

    public static void ConfigureApplication(this IServiceCollection services) =>
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies() );

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