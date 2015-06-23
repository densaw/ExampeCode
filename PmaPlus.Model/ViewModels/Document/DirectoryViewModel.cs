using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Document
{
    public class DirectoryViewModel
    {
        public string Name { get; set; }
        public IList<Role> Roles { get; set; }
    }
}
