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
        public Role Role { get; set; }
        public string TownCity { get; set; }
        public DateTime? BirthDay { get; set; }
        public int Age { get; set; }
        public string Mobile { get; set; }
        public DateTime? CrbDbsExpiry { get; set; }
        public DateTime? FirstAidExpiry  { get; set; }
        public DateTime LastLogin  { get; set; }
        public string ProfilePicture { get; set; }
    }
}
