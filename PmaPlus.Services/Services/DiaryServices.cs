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
        private readonly IDiaryRepository _diary;

        public DiaryServices(IDiaryRepository diary)
        {
            _diary = diary;
        }


    }
}
