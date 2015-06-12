using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class SessionAttendanceRepository : RepositoryBase<SessionAttendance>, ISessionAttendanceRepository
    {
        public SessionAttendanceRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
