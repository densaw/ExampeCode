namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Skill
    {
        public int Id { get; set; }

        public string SkillName { get; set; }

        public string VideoLink { get; set; }

        public string Description { get; set; }

        public int BallControll { get; set; }

        public int Corners { get; set; }

        public int? Crossing { get; set; }

        public int? Dribling { get; set; }

        public int? Finishing { get; set; }

        public int? FirstTouch { get; set; }

        public int? FreeKicks { get; set; }

        public int? Heading { get; set; }

        public int? Shooting { get; set; }

        public int? ThrowIns { get; set; }

        public int? Marking { get; set; }

        public int? Passing { get; set; }

        public int? Penalty { get; set; }

        public int? Tacking { get; set; }

        public int? Technique { get; set; }
    }
}
