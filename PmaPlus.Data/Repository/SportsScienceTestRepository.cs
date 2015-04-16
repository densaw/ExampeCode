using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class SportsScienceTestRepository : RepositoryBase<SportsScienceTest>, ISportsScienceTestRepository
    {
        public SportsScienceTestRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
