using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;

namespace Seventh.DGuard.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Seventh.DGuard.API", Version = "v1" });

                IncludeComments(c);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seventh.DGuard.API V1");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }

        private static void IncludeComments(SwaggerGenOptions c)
        {
            var apiDirectory = Directory.GetParent(typeof(Startup).Assembly.Location).FullName;

            var xmlFiles = Directory.GetFiles(apiDirectory, "*.xml");

            foreach (var file in xmlFiles)
            {
                c.IncludeXmlComments(file);
            }
        }
    }
}
