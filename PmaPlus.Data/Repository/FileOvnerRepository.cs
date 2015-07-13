using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class FileOvnerRepository : RepositoryBase<FileOvner>, IFileOvnerRepository
    {
        public FileOvnerRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
