using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class FileOvner
    {
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public int UserId { get; set; }
        public virtual User User { get;set; }
    }
}
