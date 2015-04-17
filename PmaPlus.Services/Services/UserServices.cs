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

        public InfoBoxViewModel GetUsersLoggedThisWeek(Role role = 0)
        {
            int clubsThisWeek, clubsLastWeek;

            int lastWeek = DateTool.GetThisWeek() > 1 ? DateTool.GetThisWeek() - 1 : 52;
            if (role != 0)
            {
                clubsThisWeek =
                    _userRepository.GetAll().ToList().Where(u => u.Role == role && DateTool.GetWeekNumber(u.CreateAt) == DateTool.GetThisWeek()).Count();
                clubsLastWeek =
                    _userRepository.GetAll().ToList().Where(u => u.Role == role && DateTool.GetWeekNumber(u.CreateAt) == lastWeek).Count();
            }
            else
            {
                clubsThisWeek =
                        _userRepository.GetAll().ToList().Where(u => DateTool.GetWeekNumber(u.CreateAt) == DateTool.GetThisWeek()).Count();
                clubsLastWeek =
                        _userRepository.GetAll().ToList().Where(u => DateTool.GetWeekNumber(u.CreateAt) == lastWeek).Count();
            }
            int percent;
            int progress = 0;
            if (clubsLastWeek == 0)
            {
                percent = 1000;
            }
            else
            {
                percent = (int)(((double)clubsThisWeek / clubsLastWeek) * 100);
                if (percent > 1000)
                {
                    percent = 1000;
                }
            }
            if (clubsThisWeek - clubsLastWeek > 0)
                progress = 1;
            else if (clubsThisWeek - clubsLastWeek < 0)
                progress = -1;
            

                return new InfoBoxViewModel()
                {
                    Amount = clubsThisWeek,
                    Progress = progress,
                    Percentage = percent
                };
        }



        public IList<int> GetUsersLoggedForLast_Weeks(Role role = 0, int times = 10)
        {
            List<int> usersList = new List<int>();
            int thisYear = DateTime.Now.Year;
            int thisWeek = DateTool.GetThisWeek();
            for (int i = 0; i < times; i++)
            {
                if (thisWeek < 1)
                {
                    thisWeek = 52;
                    thisYear--;
                }
                if (role != 0)
                {
                    usersList.Add(_userRepository.GetAll().ToList().Where(u =>
                             u.Role == role &&
                             DateTool.GetWeekNumber(u.CreateAt) == thisWeek &&
                             u.CreateAt.Year == thisYear).Count());
                }
                else
                {
                    usersList.Add(_userRepository.GetAll().ToList().Where(u =>
                            DateTool.GetWeekNumber(u.CreateAt) == thisWeek &&
                            u.CreateAt.Year == thisYear).Count());
                }
                thisWeek--;
            }
            usersList.Reverse();
            return usersList;

        }

    }
}
