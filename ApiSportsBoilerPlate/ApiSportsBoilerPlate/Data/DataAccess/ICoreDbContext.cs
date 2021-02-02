using ApiSportsBoilerPlate.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiSportsBoilerPlate.Data.DataAccess
{
    public interface ICoreDbContext
    {
        DbSet<Person> Person { get; set; }
        DbSet<Club> Club { get; set; }
        DbSet<PersonClub> PersonClub { get; set; }
    }
}