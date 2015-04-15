using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class PlayerAttributeRepository : RepositoryBase<PlayerAttribute>
    {
        public PlayerAttributeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
