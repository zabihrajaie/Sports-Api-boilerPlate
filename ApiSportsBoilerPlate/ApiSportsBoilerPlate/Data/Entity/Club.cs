using System.Collections.Generic;
using ApiSportsBoilerPlate.Contracts;

namespace ApiSportsBoilerPlate.Data.Entity
{
    public class Club : EntityBase<int>, IArchivebleEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public bool IsDeleted { get; set; }


        public virtual ICollection<PersonClub> PersonClub { get; set; }
    }
}