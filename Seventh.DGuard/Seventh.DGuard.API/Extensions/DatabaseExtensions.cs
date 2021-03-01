using Microsoft.Extensions.DependencyInjection;
using Seventh.DGuard.Domain.Repositories;
using Seventh.DGuard.Infra.Data.File.Repositories;
using Seventh.DGuard.Infra.Data.Sql.Repositories;

namespace Seventh.DGuard.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServerRepository, ServerRepository>();

            services.AddScoped<IVideoRepository, VideoRepository>();

            services.AddScoped<IRecyclerRepository, RecyclerRepository>();

            return services;
        }
    }
}
