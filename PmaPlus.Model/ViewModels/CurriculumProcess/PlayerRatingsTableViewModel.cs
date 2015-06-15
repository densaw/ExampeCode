using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.CurriculumProcess
{
    public class PlayerRatingsTableViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public decimal Atl { get; set; }
        public decimal Att { get; set; }
        public decimal Cur { get; set; }
    }
}
