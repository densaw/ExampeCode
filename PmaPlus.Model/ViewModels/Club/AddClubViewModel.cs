using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.Club
{
    public class AddClubViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public ClubStatus Status { get; set; }
        public string ClubAdminName { get; set; }
        public string ClubAdminUsername { get; set; }
        public string ClubAdminPassword { get; set; }
        public string Background { get; set; }
        public int Established { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string TownCity { get; set; }
        public int PostCode { get; set; }
        public string Chairman { get; set; }
        public string ChairmanEmail { get; set; }
        public string ChairmanTelephone { get; set; }
        public string WelfareOfficer { get; set; }
        public string WelfareOfficerEmail { get; set; }
        public string WelfareOfficerTelephone { get; set; }
    }
}
