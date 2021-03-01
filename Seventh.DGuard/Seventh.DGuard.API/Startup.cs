using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Seventh.DGuard.API.Extensions;
using Seventh.DGuard.Application;
using Seventh.DGuard.Infra.Data.File.Options;
using Seventh.DGuard.Infra.Data.Sql;

namespace Seventh.DGuard.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressMapClientErrors = true;
                });

            services.AddDbContext<DGuardContext>(x => x.UseSqlServer(Configuration.GetConnectionString("Database")), ServiceLifetime.Scoped);

            services.Configure<FileRepositoryOptions>(opt => Configuration.GetSection(nameof(FileRepositoryOptions)).Bind(opt));

            services.AddRepositories();

            services.AddSwagger();

            services.AddCommandValidators();

            services.AddMediatR(typeof(ApplicationModule).Assembly);

            services.AddHangfire(config => config.UseMemoryStorage());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DGuardContext databaseContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
             
                app.UseSwaggerDocs();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireServer();

            databaseContext.Database.Migrate();
        }
    }
}
