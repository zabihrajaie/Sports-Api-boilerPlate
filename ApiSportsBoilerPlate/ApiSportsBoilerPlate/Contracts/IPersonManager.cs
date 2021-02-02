using ApiSportsBoilerPlate.Data;
using ApiSportsBoilerPlate.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSportsBoilerPlate.Contracts
{
    public interface IPersonManager : IRepository<Person>
    {
        Task<(IEnumerable<Person> Persons, Pagination Pagination)> GetPersonsAsync(UrlQueryParameters urlQueryParameters);

        //Add more class specific methods here when neccessary
    }
}
