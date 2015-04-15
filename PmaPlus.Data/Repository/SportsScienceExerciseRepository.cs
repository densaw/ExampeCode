using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class SportsScienceExerciseRepository : RepositoryBase<SportsScienceExercise>
    {
        public SportsScienceExerciseRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
