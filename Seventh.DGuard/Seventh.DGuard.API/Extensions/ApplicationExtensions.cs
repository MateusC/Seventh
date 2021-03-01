using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Application.CommandValidators;

namespace Seventh.DGuard.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddCommandValidators(this IServiceCollection services)
        {
            services
                .AddScoped<CreateServerCommandValidator>()
                .AddScoped<UpdateServerCommandValidator>();

            return services;
        }
    }
}
