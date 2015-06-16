using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class PlayerMatchStatisticRepository : RepositoryBase<PlayerMatchStatistic>, IPlayerMatchStatisticRepository
    {
        public PlayerMatchStatisticRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
