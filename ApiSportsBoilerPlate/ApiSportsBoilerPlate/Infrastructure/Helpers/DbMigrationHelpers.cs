using System;
using System.Linq;
using System.Threading.Tasks;
using ApiSportsBoilerPlate.Configuration;
using ApiSportsBoilerPlate.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiSportsBoilerPlate.Infrastructure.Helpers
{
    public static class DbMigrationHelpers
    {
        public static async Task ApplyDbMigrationsWithDataSeedAsync<TDbContext>(IHost host,
            bool applyDbMigrationWithDataSeedFromProgramArguments, bool applySeed, bool applyDatabaseMigrations)
            where TDbContext : DbContext, ICoreDbContext
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                if ((applyDatabaseMigrations)
                    || (applyDbMigrationWithDataSeedFromProgramArguments))
                {
                    await EnsureDatabasesMigratedAsync<TDbContext>(services);
                }

                if ((applySeed)
                    || (applyDbMigrationWithDataSeedFromProgramArguments))
                {
                    await EnsureSeedDataAsync<TDbContext>(services);
                }
            }
        }

        public static async Task EnsureDatabasesMigratedAsync<TDbContext>(IServiceProvider services)
            where TDbContext : DbContext
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<TDbContext>())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }

        public static async Task EnsureSeedDataAsync<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : DbContext, ICoreDbContext
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

                var rootConfiguration = new SportsDataConfiguration();

                await EnsureSeedIdentityData(context, rootConfiguration);
            }
        }

        /// <summary>
        /// Generate default admin user / role
        /// </summary>
        private static async Task EnsureSeedIdentityData<TDbContext>(TDbContext context,
            SportsDataConfiguration sportsDataConfiguration)
            where TDbContext : DbContext, ICoreDbContext
        {
            // adding roles from seed
            foreach (var r in sportsDataConfiguration.People.Where(r =>
                !context.Person.Any(person => person.FirstName == r.FirstName && person.LastName == r.LastName)))
            {
                await context.Person.AddAsync(r);
                await context.SaveChangesAsync();
            }

            // adding users from seed
            foreach (var club in sportsDataConfiguration.Clubs.Where(
                club => !context.Club.Any(c => c.Name == club.Name)))
            {
                await context.Club.AddAsync(club);
                await context.SaveChangesAsync();
            }
        }
    }
}
