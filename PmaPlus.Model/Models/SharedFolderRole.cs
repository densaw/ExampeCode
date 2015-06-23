using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class SharedFolderRole
    {
        [Key,Column(Order = 0)]
        public int FolderId { get; set; }
        [Key, Column(Order = 1)]
        public Role Role { get; set; }
        public virtual SharedFolder Folder { get; set; }
    }
}
