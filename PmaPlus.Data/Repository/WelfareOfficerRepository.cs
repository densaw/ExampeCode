using System.Collections.Generic;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class WelfareOfficerRepository : RepositoryBase<WelfareOfficer>, IWelfareOfficerRepository
    {
        public WelfareOfficerRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }

}
