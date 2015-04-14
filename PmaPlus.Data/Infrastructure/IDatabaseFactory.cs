using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Data
{
    public interface IDatabaseFactory
    {
         DataBaseContext Get();
    }
}
