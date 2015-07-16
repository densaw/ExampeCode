using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmaPlus.Model.Models
{
    public class SessionAttendanceDetail
    {
        [Key, ForeignKey("SessionResult")]
        public int SessionResultId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public virtual SessionResult SessionResult { get; set; }
    }
}
