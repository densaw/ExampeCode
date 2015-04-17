using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data;
using PmaPlus.Data.Infrastructure;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.ViewModels;

namespace PmaPlus.Services
{
    public class UserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }

        public InfoBoxViewModel GetUsersLoggedThisWeek(Role role)
        {
            int clubsThisWeek =
                _userRepository.GetAll().ToList().Where(u => u.Role == role && DateTool.GetWeekNumber(u.CreateAt) == DateTool.GetThisWeek()).Count();
            int lastWeek = DateTool.GetThisWeek() > 1 ? DateTool.GetThisWeek() - 1 : 52;
            int clubsLastWeek =
                _userRepository.GetAll().ToList().Where(u => u.Role == role && DateTool.GetWeekNumber(u.CreateAt) == lastWeek).Count();
            return new InfoBoxViewModel()
            {
                Amount = clubsThisWeek,
                Progress = clubsThisWeek > clubsLastWeek
            };
        }

        public InfoBoxViewModel GetUsersLoggedThisWeek()
        {
            int clubsThisWeek =
                _userRepository.GetAll().ToList().Where(u => DateTool.GetWeekNumber(u.CreateAt) == DateTool.GetThisWeek()).Count();
            int lastWeek = DateTool.GetThisWeek()  > 1 ? DateTool.GetThisWeek() - 1 : 52;
            int clubsLastWeek =
                _userRepository.GetAll().ToList().Where(u => DateTool.GetWeekNumber(u.CreateAt) == lastWeek).Count();
            return new InfoBoxViewModel()
            {
                Amount = clubsThisWeek,
                Progress = clubsThisWeek > clubsLastWeek
            };
        }
        
    }
}
