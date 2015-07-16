using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.CurriculumProcess
{
    public class CurriculumPlayersStatisticViewModel
    {
        public string PlayerPicture { get; set; }   
        public string PlayerName { get; set; }
        public int Age { get; set; }
        public decimal Atl { get; set; }
        public decimal Att { get; set; }
        public int Mom { get; set; }
        public double Gls { get; set; }
        public double Sho { get; set; }
        public double Sht { get; set; }
        public double Asi { get; set; }
        public double Tck { get; set; }
        public double Pas { get; set; }
        public double Sav { get; set; }
        public double Crn { get; set; }
        public double Frk { get; set; }
        public double Frm { get; set; }
        public int Inj { get; set; }
        public decimal AttPercent { get; set; }
        public decimal WbPercent { get; set; }
        public decimal Cur { get; set; }

        public int TT { get; set; }
        public int PT { get; set; }
        public decimal TTPercent { get; set; }
        public decimal PTPercent { get; set; }

    }
}
