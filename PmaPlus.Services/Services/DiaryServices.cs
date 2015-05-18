using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;

namespace PmaPlus.Services
{
    public class DiaryServices
    {
        private readonly IDiary _diary;

        public DiaryServices(IDiary diary)
        {
            _diary = diary;
        }


    }
}
