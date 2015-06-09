using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.TalentIdentifications
{
    public class TalentNoteViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Scout { get; set; }
        public DateTime AddDate { get; set; }
        public string Location { get; set; }
        [Required]
        public string Note { get; set; }

        public int TalentIdentificationId { get; set; }

    }
}
