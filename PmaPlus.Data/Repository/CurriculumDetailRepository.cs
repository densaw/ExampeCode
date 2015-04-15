using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    class CurriculumDetailRepository : RepositoryBase<CurriculumDetail>
    {
        public CurriculumDetailRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
