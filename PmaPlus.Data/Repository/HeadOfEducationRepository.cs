﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    class HeadOfEducationRepository : RepositoryBase<HeadOfEducation>, IHeadOfEducationRepository
    {
        public HeadOfEducationRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
