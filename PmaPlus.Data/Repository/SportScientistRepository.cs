using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class SportScientistRepository : RepositoryBase<SportScientist>, ISportScientistRepository
    {
        public SportScientistRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
