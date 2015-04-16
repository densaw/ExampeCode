using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class SportsScienceExerciseRepository : RepositoryBase<SportsScienceExercise>, ISportsScienceExerciseRepository
    {
        public SportsScienceExerciseRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
