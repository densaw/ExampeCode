using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.TalentIdentifications
{
    public class TalentIdentificationViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int ScoutId { get; set; }
        public DateTime DateIdentificated { get; set; }
        [Required]
        public string PresentClub { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string ParentsFirstName { get; set; }
        [Required]
        public string ParentsLastName { get; set; }
        [Required]
        public string ParentsEmail { get; set; }
        [Required]
        public string ParentsMobile { get; set; }
        [Required]
        public string ParentsNote { get; set; }
        
    }
}
