using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels
{
    public abstract class Page
    {
        public  int Count { get; set; }
        public  int Pages { get; set; }

    }
}
