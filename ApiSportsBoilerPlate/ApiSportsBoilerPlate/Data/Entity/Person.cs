using System;
using System.Collections.Generic;

namespace ApiSportsBoilerPlate.Data.Entity
{
    public class Person : EntityBase<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<PersonClub> PersonClub { get; set; }
    }
}
