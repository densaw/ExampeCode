using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model;

namespace PmaPlus.Data
{
    public class UserRepository : RepositoryBase<User>,IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

       
       
    }

 
}
