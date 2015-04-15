using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{


    public class PhysiotherapyExerciseRepository : RepositoryBase<PhysiotherapyExercise>
    {
        public PhysiotherapyExerciseRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
