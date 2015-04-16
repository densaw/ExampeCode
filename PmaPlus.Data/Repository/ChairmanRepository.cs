using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    class ChairmanRepository : RepositoryBase<Chairman>, IChairmanRepository
    {
        public ChairmanRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
