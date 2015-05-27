using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class ScenarioSession
    {
        [Key,Column(Order = 0)]
        public int SessionId { get; set; }
        [Key, Column(Order = 1)]
        public int ScenarioId { get; set; }
        public virtual Session Session { get; set; }
        public virtual Scenario Scenario { get; set; }
    }
}
