using System;
using System.Net.Http;
using System.Threading.Tasks;
using Beer.Countdown.Web.Countdown;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Beer.Countdown.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.BrowserConsole()
                .CreateLogger();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Blazorise, bootstrap, font awesome
            builder.Services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true;
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();

            builder.Services.AddTransient(ctx => new CountdownConfiguration
            {
                SchoolsOpeningDate = new DateTime(2021, 03, 08),
                SixPeopleOutsideDate = new DateTime(2021, 03, 29),
                NonEssentialShopsOpeningDate = new DateTime(2021, 04, 12),
                SixPeopleInsideDate = new DateTime(2021, 05, 17),
                RestrictionsLifted = new DateTime(2021, 06, 21)
            });
            builder.Services.AddTransient<ICountdownService, CountdownService>();

            var host = builder.Build();

            host.Services
              .UseBootstrapProviders()
              .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
