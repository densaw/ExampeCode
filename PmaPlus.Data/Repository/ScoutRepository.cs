using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class ScoutRepository : RepositoryBase<Scout>
    {
        public ScoutRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
