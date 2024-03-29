using BlazingTrails.Client.State;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazingTrails.Client
{
    public static class Extension
    {
        public static void RegisterServices(this IServiceCollection services, WebAssemblyHostBuilder builder)
        {
            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            services.AddScoped<AppState>();
        }
    }
}
