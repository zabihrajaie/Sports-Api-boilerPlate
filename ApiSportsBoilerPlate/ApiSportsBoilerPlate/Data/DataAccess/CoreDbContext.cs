using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ApiSportsBoilerPlate.Contracts;
using ApiSportsBoilerPlate.Data.Entity;
using ApiSportsBoilerPlate.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ApiSportsBoilerPlate.Data.DataAccess
{
    public class CoreDbContext : DbContext, ICoreDbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options) { }

        public DbSet<Person> Person { get; set; }
        public DbSet<Club> Club { get; set; }
        public DbSet<PersonClub> PersonClub { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PersonClub>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.PersonClub)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonClub)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #region Ef Global Filters
            // single entity
            //modelBuilder.Entity<Place>().HasQueryFilter(p => !p.IsDeleted);

            // all entities
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IArchivebleEntity).IsAssignableFrom(type.ClrType)
                    && (type.BaseType == null || !typeof(IArchivebleEntity).IsAssignableFrom(type.BaseType.ClrType)))
                    modelBuilder.SetEfGlobalFilters(type.ClrType);
            }
            #endregion
        }

        public override int SaveChanges()
        {
            //UpdateSoftDeleteStatuses();
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            //UpdateSoftDeleteStatuses();
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //UpdateSoftDeleteStatuses();
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //UpdateSoftDeleteStatuses();
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var val = (string)property.GetValue(item.Entity, null);

                    if (!val.HasValue()) continue;

                    var newVal = val.Fa2En().FixPersianChars();
                    if (newVal == val) continue;

                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }


        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["isDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["isDeleted"] = true;
                        break;
                }
            }
        }
    }
}
