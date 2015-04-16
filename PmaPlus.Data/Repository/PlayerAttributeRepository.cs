using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class PlayerAttributeRepository : RepositoryBase<PlayerAttribute>, IPlayerAttributeRepository
    {
        public PlayerAttributeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
