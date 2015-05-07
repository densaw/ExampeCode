using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public class SkillVideo
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Duration { get; set; }
        public string VideoLink { get; set; }
        public string Description { get; set; }
        public int BallControll { get; set; }
        public int Corners { get; set; }
        public int Crossing { get; set; }
        public int Dribling { get; set; }
        public int Finishing { get; set; }
        public int FirstTouch { get; set; }
        public int FreeKicks { get; set; }
        public int Heading { get; set; }
        public int Shooting { get; set; }
        public int ThrowIns { get; set; }
        public int Marking { get; set; }
        public int Passing { get; set; }
        public int Penalty { get; set; }
        public int Tacking { get; set; }
        public int Technique { get; set; }
        public int Aggression { get; set; }     
        public int Anticipation { get; set; }   
        public int Bravery { get; set; }        
        public int Composure { get; set; }      
        public int Concentration { get; set; }  
        public int Creativity { get; set; }     
        public int Decisions { get; set; }      
        public int Determination { get; set; }  
        public int Flair { get; set; }      
        public int Influence { get; set; }      
        public int OffTheBall { get; set; }     
        public int Positioning { get; set; }    
        public int Teamwork { get; set; }
        public int Goalkeeping { get; set; }

        [Required]
        public virtual SkillLevel SkillLevel { get; set; }



    }
}
