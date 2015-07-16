using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    class SessionAttendanceDetailRepository : RepositoryBase<SessionAttendanceDetail>, ISessionAttendanceDetailRepository
    {
        public SessionAttendanceDetailRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
