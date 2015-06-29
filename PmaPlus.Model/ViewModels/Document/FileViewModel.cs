using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Document
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileType { get; set; }
        public DateTime AddDate { get; set; }
    }
}
