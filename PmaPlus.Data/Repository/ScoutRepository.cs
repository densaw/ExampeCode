using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class ScoutRepository : RepositoryBase<Scout>, IScoutRepository
    {
        public ScoutRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
