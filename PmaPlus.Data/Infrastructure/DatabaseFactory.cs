using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory 
    {
        private DataBaseContext _dataContext;
        public DataBaseContext Get()
        {
            return _dataContext ?? (_dataContext = new DataBaseContext());
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
