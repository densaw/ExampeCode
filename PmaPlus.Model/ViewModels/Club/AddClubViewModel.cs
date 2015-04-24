using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.Club
{
    public class AddClubViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Logo { get; set; }
        [Required]
        public ClubStatus Status { get; set; }
        [Required]
        public string ClubAdminName { get; set; }
        [Required]
        public string ClubAdminEmail { get; set; }
        [Required]
        public string ClubAdminPassword { get; set; }
        public string Background { get; set; }
        [Required]
        public int Established { get; set; }
        [Required]
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        [Required]
        public string TownCity { get; set; }
        [Required]
        public int PostCode { get; set; }
        public string Chairman { get; set; }
        public string ChairmanEmail { get; set; }
        [MaxLength(12)]
        public string ChairmanTelephone { get; set; }
        public string ColorTheme { get; set; }
    }
}
