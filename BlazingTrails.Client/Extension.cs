using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;

namespace BlazingTrails.CrossCutting
{
    public static class Extension
    {
        public static void AddServices(this IServiceCollection services, WebAssemblyHostBuilder builder)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.Load("BlazingTrails.Application")));
        }
    }
}
