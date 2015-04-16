using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
