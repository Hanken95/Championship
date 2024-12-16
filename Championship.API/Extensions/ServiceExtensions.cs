using Championchip.Core.Repositories;
using Championship.Data.Repositories;
using Championship.Services;
using Service.Contracts;

namespace Championship.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, string name = "Any")
        {
            if (name == "Any")
            {
                services.AddCors(builder =>
                {
                    builder.AddPolicy("AllowAny", policyBuilder =>
                        policyBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        );
                });
            }
        }

        public static void ConfigureServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScopedLazy<ITournamentService>();
            services.AddScopedLazy<IGameService>();

            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IGameService, GameService>();
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScopedLazy<ITournamentRepository>();
            services.AddScopedLazy<IGameRepository>();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScopedLazy<TService>(this IServiceCollection services) where TService: class
        {
            return services.AddScoped(iSProvider => new Lazy<TService>(() => iSProvider.GetRequiredService<TService>()));
        }
    }
}

