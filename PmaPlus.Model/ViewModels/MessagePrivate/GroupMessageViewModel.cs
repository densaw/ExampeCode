using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.MessagePrivate
{
    public class GroupMessageViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime SendAt { get; set; }
        public int UserId { get; set; }
    }
}
