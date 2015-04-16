using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{


    public class PhysiotherapyExerciseRepository : RepositoryBase<PhysiotherapyExercise>, IPhysiotherapyExerciseRepository
    {
        public PhysiotherapyExerciseRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
