using BasicProject.API.Extensions;
using BasicProject.Domain.Config;
using BasicProject.Infra.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;

namespace BasicProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<BasicProjectContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var settings = services.GetService<IOptions<BasicProjectSettings>>();
                }).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddEnvironmentVariables();
                }).ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Log4NetCore"));
                    //builder.AddLog4Net();
                    builder.SetMinimumLevel(LogLevel.Debug);
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .Build();
    }
}
