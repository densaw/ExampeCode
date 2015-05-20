using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class CurriculumStatement
    {
        public int Id { get; set; }
        public virtual ICollection<StatementRoles> Roles { get; set; }
        public bool ChooseBlock { get; set; }
        public bool ChooseWeek { get; set; }
        public bool ChooseSession { get; set; }
        public string Statement { get; set; }
    }
}
