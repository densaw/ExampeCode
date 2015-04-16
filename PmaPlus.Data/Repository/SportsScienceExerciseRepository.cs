using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class SportsScienceExerciseRepository : RepositoryBase<SportsScienceExercise>, ISportsScienceExerciseRepository
    {
        public SportsScienceExerciseRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
