using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;

namespace PmaPlus.Model.ViewModels.Player
{
    public class PlayerTableForScoutViewModel
    {
        public int Id { get; set; }
        public DateTime DateFound { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ParentsName { get; set; }
        public string Mobile { get; set; }
        public decimal AttributeRating { get; set; }
        public string PlayerClub { get; set; }
    }
}
