using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class SportsScienceTestRepository : RepositoryBase<SportsScienceTest>
    {
        public SportsScienceTestRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
