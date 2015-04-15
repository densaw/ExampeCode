using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class SkillRepository : RepositoryBase<Skill>
    {
        public SkillRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
