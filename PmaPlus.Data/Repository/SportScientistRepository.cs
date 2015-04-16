using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class SportScientistRepository : RepositoryBase<SportScientist>, ISportScientistRepository
    {
        public SportScientistRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
