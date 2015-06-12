using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class BlockObjectiveStatementRepository : RepositoryBase<BlockObjectiveStatement>, IBlockObjectiveStatementRepository
    {
        public BlockObjectiveStatementRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
