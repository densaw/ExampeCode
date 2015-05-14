using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class CurriculumSession
    {
        public int Id { get; set; }

        public virtual CurriculumWeek CurriculumWeek { get; set; }
        public virtual CurriculumDetail CurriculumDetail { get; set; }
    }
}
