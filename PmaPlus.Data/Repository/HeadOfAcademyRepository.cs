using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    class HeadOfAcademyRepository : RepositoryBase<HeadOfAcademy>, IHeadOfAcademyRepository
    {
        public HeadOfAcademyRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
