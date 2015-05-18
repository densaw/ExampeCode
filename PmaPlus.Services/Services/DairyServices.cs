using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class DairyServices
    {
        private readonly IDiaryRepository _diaryRepository;
        private readonly IUserRepository _userRepository;

        public DairyServices(IDiaryRepository diaryRepository, IUserRepository userRepository)
        {
            _diaryRepository = diaryRepository;
            _userRepository = userRepository;
        }

        public Diary AddDiary(Diary diary, int userId)
        {
            //TODO:add diary
            return null;
        }



    }
}
