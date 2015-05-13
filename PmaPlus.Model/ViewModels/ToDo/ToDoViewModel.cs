using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.ToDo
{
    public class ToDoViewModel
    {
        public int Id { get; set; }
        [Required,MaxLength(20)]
        public string Title { get; set; }
        public ToDoPriority Priority { get; set; }
        public bool Complete { get; set; }
        public DateTime CompletionDateTime { get; set; }
        [Required, MaxLength(229)]
        public string Note { get; set; }
    }
}
