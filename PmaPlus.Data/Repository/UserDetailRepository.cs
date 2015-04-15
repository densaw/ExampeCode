using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class UserDetailRepository : RepositoryBase<UserDetail>
    {
        public UserDetailRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
