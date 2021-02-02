using ApiSportsBoilerPlate.Infrastructure.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApiSportsBoilerPlate.Data.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ApiSportsBoilerPlate
{
    public class Program
    {
        private const string SeedArgs = "/seed";
        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            var logger = builder.Services.GetService<ILogger<Program>>();

            try
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                logger.LogInformation("Starting web host");

                var host = CreateHostBuilder(args).Build();

                await ApplyDbMigrationsWithDataSeedAsync(args, config, host);

                builder.Run();

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Host unexpectedly terminated");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configBuilder =>
                    configBuilder.AddJsonFile("appsettings.Logs.json", true, true)
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .UseSerilog((hostingContext, loggerConfig) =>
                    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
                );


        private static async Task ApplyDbMigrationsWithDataSeedAsync(string[] args, IConfigurationRoot configuration, IHost host)
        {
            var applyDbMigrationWithDataSeedFromProgramArguments = args.Any(x => x == SeedArgs);
            if (applyDbMigrationWithDataSeedFromProgramArguments) args = args.Except(new[] { SeedArgs }).ToArray();

            bool.TryParse(configuration.GetSection("SeedConfiguration:ApplySeed").Value, out var applySeed);
            bool.TryParse(configuration.GetSection("DatabaseMigrationsConfiguration:ApplyDatabaseMigrations").Value, out var applyDatabaseMigrations);

            await DbMigrationHelpers
                .ApplyDbMigrationsWithDataSeedAsync<CoreDbContext>(host,
                    applyDbMigrationWithDataSeedFromProgramArguments, applySeed, applyDatabaseMigrations);
        }
    }
}
