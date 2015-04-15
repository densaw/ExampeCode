using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    class BodyPartRepository : RepositoryBase<BodyPart>
    {
        public BodyPartRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
