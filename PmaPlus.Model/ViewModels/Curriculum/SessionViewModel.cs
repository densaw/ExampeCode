using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }           
        [Required]      
        public string Name { get; set; }                        
        public bool Attendance { get; set; }                    
        public bool Objectives { get; set; }                    
        public bool Rating { get; set; }                        
        public bool Report { get; set; }                        
        public bool ObjectiveReport { get; set; }               
        public bool CoachDetails { get; set; }
        public bool StartOfReviewPeriod { get; set; }
        public bool EndOfReviewPeriod { get; set; }   
        public string CoachPicture { get; set; }                
        public string CoachDetailsName { get; set; }            
        public bool PlayerDetails { get; set; }                 
        public string PlayerPicture { get; set; }               
        public string PlayerDetailsName { get; set; }           
        public bool NeedScenarios { get; set; }                 
        public IList<int> Scenarios { get; set; }               
    }
}
