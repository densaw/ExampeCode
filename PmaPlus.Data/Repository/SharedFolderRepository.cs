using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class SharedFolderRepository : RepositoryBase<SharedFolder>, ISharedFolderRepository
    {
        public SharedFolderRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
