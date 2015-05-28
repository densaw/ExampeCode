using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int Number { get; set; }                                          
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
        public string  PlayerPicture { get; set; }                               
        public string PlayerDetailsName { get; set; }                            
        public bool NeedScenarios { get; set; }                                  
        public virtual ICollection<Scenario> Scenarios { get; set; }             
        public virtual Curriculum Curriculum { get; set; }
    }
}
