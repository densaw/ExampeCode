using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Models;

namespace PmaPlus.Model.ViewModels
{
    public class FaCoursePage : Page
    {
        public IQueryable<FACourse> Items { get; set; }
    }
}
