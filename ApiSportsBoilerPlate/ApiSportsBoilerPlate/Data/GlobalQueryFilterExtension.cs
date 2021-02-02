using System;
using System.Linq;
using System.Reflection;
using ApiSportsBoilerPlate.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ApiSportsBoilerPlate.Data
{
    public static class GlobalQueryFilterExtension
    {
        public static void SetEfGlobalFilters(this ModelBuilder modelBuilder, Type entityType)
        {
            SetEfGlobalFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        private static readonly MethodInfo SetEfGlobalFilterMethod = typeof(GlobalQueryFilterExtension)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetEfGlobalFilters));

        public static void SetEfGlobalFilters<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, IArchivebleEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
