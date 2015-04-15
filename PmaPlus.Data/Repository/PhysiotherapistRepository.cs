using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{


    public class PhysiotherapistRepository : RepositoryBase<Physiotherapist>
    {
        public PhysiotherapistRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
