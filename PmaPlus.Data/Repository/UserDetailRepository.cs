using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class UserDetailRepository : RepositoryBase<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
