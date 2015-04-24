using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class SkillVideoRepository : RepositoryBase<SkillVideo>, ISkillVideoRepository
    {
        public SkillVideoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
