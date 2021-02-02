using System.Collections;
using System.Collections.Generic;
using ApiSportsBoilerPlate.Contracts;

namespace ApiSportsBoilerPlate.Data.Entity
{
    public class PersonClub : EntityBase<int>, IArchivebleEntity
    {
        public int ClubId { get; set; }
        public int PersonId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Club Club { get; set; }
        public virtual Person Person { get; set; }
    }
}