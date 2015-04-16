using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class SportsScienceTestRepository : RepositoryBase<SportsScienceTest>, ISportsScienceTestRepository
    {
        public SportsScienceTestRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
