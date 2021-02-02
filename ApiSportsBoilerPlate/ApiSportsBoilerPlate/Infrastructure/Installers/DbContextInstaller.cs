using ApiSportsBoilerPlate.Contracts;
using ApiSportsBoilerPlate.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSportsBoilerPlate.Infrastructure.Installers
{
    public class DbContextInstaller : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoreDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SQLDBConnectionString"), x => x.UseNetTopologySuite()));
        }
    }
}