﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumStatementPage : Page
    {
        public IEnumerable<CurriculumStatementViewModel> Items { get; set; }
    }
}
