using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.TrainingTeamMember
{
    public class TrainingTeamMemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TownCity { get; set; }
        public string  PostCode { get; set; }
        public string Mobile { get; set; }
        public string AboutMe { get; set; }                                 
        public string ProfilePicture { get; set; }
    }
}
