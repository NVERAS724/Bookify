using Bookify.Application.Abstractions.Clock;
using Bookify.Infrastructure.Clock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
