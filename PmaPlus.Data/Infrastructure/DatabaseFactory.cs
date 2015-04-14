using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Data.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private DataBaseContext _dataContext;
        public DataBaseContext Get()
        {
            return _dataContext ?? (_dataContext = new DataBaseContext());
        }
        
    }
}
