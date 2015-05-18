using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository.Iterfaces
{
    public class DiaryRepository : RepositoryBase<Diary>, IDiaryRepository
    {
        public DiaryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
