using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSportsBoilerPlate.Data.Entity
{
    public interface IEntity
    {
    }

    public class EntityBase<TKey> : IEntity
    {
        //Add common Properties here that will be used for all your entities
        public TKey Id { get; set; }
        public long RowCreatedById { get; set; }
        public long RowModifiedById { get; set; }
        public DateTime RowCreatedDateTimeUtc { get; set; }
        public DateTime RowModifiedDateTimeUtc { get; set; }
    }

    public abstract class EntityBase : EntityBase<long>
    {
    }
}
