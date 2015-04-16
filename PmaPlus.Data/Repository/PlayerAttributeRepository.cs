using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class PlayerAttributeRepository : RepositoryBase<PlayerAttribute>, IPlayerAttributeRepository
    {
        public PlayerAttributeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
