using BlazingTrails.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingTrails.Infra
{
    public static class Extension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add sql server and connection string
            services.AddDbContext<BlazingTrailsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DBTrailsContext")));
        }
    }
}
