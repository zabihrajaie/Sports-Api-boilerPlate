namespace ApiSportsBoilerPlate.Contracts
{
    public interface IArchivebleEntity
    {
        bool IsDeleted { get; set; }
    }
}