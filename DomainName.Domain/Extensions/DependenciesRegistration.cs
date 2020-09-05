using DomainName.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Text;
// microsoft
using Microsoft.Extensions.DependencyInjection;
using DomainName.Domain.Services;

namespace DomainName.Domain.Extensions
{
    public static class DependenciesRegistration
    {

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services
                .AddSingleton<IUserMapper, UserMapper>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<ICustomAuthenticationManager, CustomAuthenticationManager>();

            return services;
        }

    }
}
