using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public  class Club
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Logo { get; set; }
        public string ColorTheme { get; set; }
        public string Background { get; set; }
        public ClubStatus Status { get; set; }
        public int Established { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual Address Address { get; set; }
        public virtual Chairman Chairman { get; set; }

       
        public virtual ClubAdmin ClubAdmin { get; set; }
        public virtual Physiotherapist Physiotherapist { get; set; }
        public virtual WelfareOfficer WelfareOfficer { get; set; }
        public virtual ICollection<Coach> Coaches { get; set; }
        public virtual ICollection<HeadOfEducation> HeadOfEducations { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<SportScientist> SportScientists { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        
    }
}
