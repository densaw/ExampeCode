using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumStatementViewModel
    {
        public int Id { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public string Statement { get; set; }
    }
}
