using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels
{
    public class ScenarioViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ScenarioType ScenarioType { get; set; }
        [Required]
        public int MinAge { get; set; }
        [Required]
        public int MaxAge { get; set; }
        //[Required]
        public string Picture { get; set; }
        [Required]
        public string Description { get; set; }
        
        public string Video { get; set; }

        public bool Share { get; set; }
    }
}
