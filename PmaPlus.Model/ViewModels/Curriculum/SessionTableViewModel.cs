using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class SessionTableViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }                         
        public string Name { get; set; }                        
        public bool Attendance { get; set; }                    
        public bool Objectives { get; set; }                    
        public bool Rating { get; set; }                        
        public bool Report { get; set; }
        public bool StartOfReviewPeriod { get; set; }
        public bool EndOfReviewPeriod { get; set; }

        
    }
}
