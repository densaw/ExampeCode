using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class ActivityStatusChange
    {
        public int Id { get; set; }


        public DateTime DateTime { get; set; }

        public int  ActiveCount { get; set; }
    }
}
