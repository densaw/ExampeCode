using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class SkillRepository : RepositoryBase<Skill>, ISkillRepository
    {
        public SkillRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
