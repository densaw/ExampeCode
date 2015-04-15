using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class TeamRepository : RepositoryBase<Team>
    {
        public TeamRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
