using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class PlayerMatchObjectiveRepository : RepositoryBase<PlayerMatchObjective>, IPlayerMatchObjectiveRepository
    {
        public PlayerMatchObjectiveRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
