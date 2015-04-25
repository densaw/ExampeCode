using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Skill
{
    public class SkillVideoViewModel
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0,100)]
        public int Duration { get; set; }
        [Required]
        [Range(0, 100)]    
        public string VideoLink { get; set; }
        [Required]
        [Range(0, 100)]
        public string Description { get; set; }
        [Required]
        [Range(0, 100)]
        public int BallControll { get; set; }
        [Required]
        [Range(0, 100)]
        public int Corners { get; set; }
        [Required]
        [Range(0, 100)]
        public int Crossing { get; set; }
        [Required]
        [Range(0, 100)]
        public int Dribling { get; set; }
        [Required]
        [Range(0, 100)]
        public int Finishing { get; set; }
        [Required]
        [Range(0, 100)]
        public int FirstTouch { get; set; }
        [Required]
        [Range(0, 100)]
        public int FreeKicks { get; set; }
        [Required]
        [Range(0, 100)]
        public int Heading { get; set; }
        [Required]
        [Range(0, 100)]
        public int Shooting { get; set; }
        [Required]
        [Range(0, 100)]
        public int ThrowIns { get; set; }
        [Required]
        [Range(0, 100)]
        public int Marking { get; set; }
        [Required]
        [Range(0, 100)]
        public int Passing { get; set; }
        [Required]
        [Range(0, 100)]
        public int Penalty { get; set; }
        [Required]
        [Range(0, 100)]
        public int Tacking { get; set; }
        [Required]
        [Range(0, 100)]
        public int Technique { get; set; }
        [Required]
        [Range(0, 100)]
        public int Aggression { get; set; }
        [Required]
        [Range(0, 100)]
        public int Anticipation { get; set; }
        [Required]
        [Range(0, 100)]
        public int Bravery { get; set; }
        [Required]
        [Range(0, 100)]
        public int Composure { get; set; }
        [Required]
        [Range(0, 100)]
        public int Concentration { get; set; }
        [Required]
        [Range(0, 100)]
        public int Creativity { get; set; }
        [Required]
        [Range(0, 100)]
        public int Decisions { get; set; }
        [Required]
        [Range(0, 100)]
        public int Determination { get; set; }
        [Required]
        [Range(0, 100)]
        public int Flair { get; set; }
        [Required]
        [Range(0, 100)]
        public int Influence { get; set; }
        [Required]
        [Range(0, 100)]
        public int OffTheBall { get; set; }
        [Required]
        [Range(0, 100)]
        public int Positioning { get; set; }
        [Required]
        [Range(0, 100)]
        public int Teamwork { get; set; }
    }


}
