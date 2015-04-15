using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class PlayerRepository : RepositoryBase<Player>
    {
        public PlayerRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
