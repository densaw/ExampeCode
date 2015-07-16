using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Player
{
    public class PlayerTableViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public IEnumerable<string> Teams { get; set; }
        public int Age { get; set; }
        public int Mom { get; set; }
        public decimal Gls { get; set; }
        public decimal Frm { get; set; }
        public int Inj { get; set; }
        public decimal Att { get; set; }
        public decimal Atl { get; set; }
        public int Wb { get; set; }
        public decimal WbPercent { get; set; }
        public decimal Cur { get; set; }
        public decimal AttPercent { get; set; }

        public int TT { get; set; }
        public int PT { get; set; }
        public decimal TTPercent { get; set; }
        public decimal PTPercent { get; set; }
    }
}
