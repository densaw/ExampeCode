using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{


    public class PhysiotherapistRepository : RepositoryBase<Physiotherapist>, IPhysiotherapistRepository
    {
        public PhysiotherapistRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
