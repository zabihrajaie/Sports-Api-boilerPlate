using System.Threading.Tasks;

namespace ApiSportsBoilerPlate.Contracts
{
    public interface IAuthServerConnect
    {
        Task<string> RequestClientCredentialsTokenAsync();
    }
}
