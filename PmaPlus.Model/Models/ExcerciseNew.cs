using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class ExcerciseNew
    {
        public int Id { get; set; }
        public DateTime NewsDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPicture { get; set; }
        public string MainPicture { get; set; }
        public string SponsoredBy { get; set; }
        public string Picture { get; set; }
    }
}
