using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{


    public class PhysiotherapistRepository : RepositoryBase<Physiotherapist>, IPhysiotherapistRepository
    {
        public PhysiotherapistRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
