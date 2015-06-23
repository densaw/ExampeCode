using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class SharedFolder
    {
        public SharedFolder()
        {
            Roles = new HashSet<SharedFolderRole>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string FolderName { get; set; }

        public virtual ICollection<SharedFolderRole> Roles { get; set; }
        public virtual User User { get; set; }
    }
}
