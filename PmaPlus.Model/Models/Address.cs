using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string  Telephone { get; set; }
        public string  Mobile { get; set; }

        public string TownCity { get; set; }

        public int PostCode { get; set; }
    }
}
