using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Models;

namespace PmaPlus.Model.ViewModels.SiteSettings
{
    public class PasswordHistoryTableViewModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangeAt { get; set; }
        public DateTime UserLoggedAt { get; set; }
        public string Ip { get; set; }
    }
}
