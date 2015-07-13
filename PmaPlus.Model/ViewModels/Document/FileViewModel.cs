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
        public bool IsImage { get; set; }
        public DateTime AddDate { get; set; }
        public bool IsMy { get; set; }
    }
}
