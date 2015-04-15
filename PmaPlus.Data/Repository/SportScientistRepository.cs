using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class SportScientistRepository : RepositoryBase<SportScientist>
    {
        public SportScientistRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
