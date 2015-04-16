using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class ScoutRepository : RepositoryBase<Scout>, IScoutRepository
    {
        public ScoutRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
