using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MudBlazor.Services;

namespace MainApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
      
            builder.Services.AddHttpClient("CamundaAPI", client => {
                client.BaseAddress = new Uri("https://localhost:8080/engine-rest");
            });

            builder.Services.AddSingleton<MainApp.Shared.AppData>();

            builder.Services.AddOidcAuthentication(options => {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            builder.Services
                .AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
