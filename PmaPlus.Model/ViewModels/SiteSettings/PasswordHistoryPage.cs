using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.SiteSettings
{
    public class PasswordHistoryPage : Page
    {
        public IEnumerable<PasswordHistoryTableViewModel> Items { get; set; }
    }
}
