using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{


    public class PhysiotherapyExerciseRepository : RepositoryBase<PhysiotherapyExercise>, IPhysiotherapyExerciseRepository
    {
        public PhysiotherapyExerciseRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
