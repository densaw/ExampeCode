using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ToDoPriority Priority { get; set; }
        public DateTime CompletionDateTime { get; set; }
        public string Note { get; set; }

        public UserDetail UserDetail { get; set; }
    }
}
