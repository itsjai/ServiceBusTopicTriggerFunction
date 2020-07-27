
[assembly: Microsoft.Azure.Functions.Extensions.DependencyInjection.FunctionsStartup(typeof(ServiceBusTopicTrigger.Startup))]

namespace ServiceBusTopicTrigger
{
    using System;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup : FunctionsStartup
    {
        IConfiguration config = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup()
        {
            var builder = new ConfigurationBuilder()
#if (DEBUG)
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
#endif
                .AddEnvironmentVariables();

            this.config = builder.Build(); 
        }

        /// <inheritdoc/>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            TelemetryClient telemetryClient = new TelemetryClient(new TelemetryConfiguration(this.config["APPINSIGHTS_INSTRUMENTATIONKEY"]));


            builder.Services.AddSingleton<IConfiguration>(this.config);

            builder.Services.AddSingleton<ServiceBusFunction>();
        }
    }
}
