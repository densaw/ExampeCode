using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels
{
    public class ScenarioTableViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ScenarioType ScenarioType { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Picture { get; set; }

        public string UploadedBy { get; set; }
    }
}
