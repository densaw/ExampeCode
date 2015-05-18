using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository.Iterfaces
{
    class DiaryRepository : RepositoryBase<Diary>, IDiary
    {
        public DiaryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
