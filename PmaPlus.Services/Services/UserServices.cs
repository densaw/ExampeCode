using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using PmaPlus.Data;
using PmaPlus.Data.Infrastructure;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;

namespace PmaPlus.Services
{
    public class UserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IClubAdminRepository _clubAdminRepository;

        public UserServices(IUserRepository userRepository, IClubAdminRepository clubAdminRepository)
        {
            _userRepository = userRepository;
            _clubAdminRepository = clubAdminRepository;
        }

        public ClubAdmin GetClubAdminByUserName(string name)
        {
            return _clubAdminRepository.Get(a => a.User.UserName == name);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.Get(u => u.Email == email);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user,user.Id);
        }

        public InfoBoxViewModel GetUsersLoggedThisWeek(Role role = 0)
        {
            int clubsThisWeek, clubsLastWeek;

            int thisWeek = DateTool.GetThisWeek();
            int lastWeek = DateTool.GetThisWeek() > 1 ? DateTool.GetThisWeek() - 1 : 52;
            if (role != 0)
            {

                clubsThisWeek =
                    _userRepository.GetMany(u => u.Role == role && SqlFunctions.DatePart("week",u.LoggedAt) == thisWeek).Count();
                clubsLastWeek =
                    _userRepository.GetMany(u => u.Role == role && SqlFunctions.DatePart("week", u.LoggedAt) == lastWeek).Count();


            }
            else
            {
                clubsThisWeek =
                        _userRepository.GetMany(u => SqlFunctions.DatePart("week", u.LoggedAt) == thisWeek).Count();
                clubsLastWeek =
                        _userRepository.GetMany(u => SqlFunctions.DatePart("week", u.LoggedAt) == lastWeek).Count();
            }
            int percent;
            string progress = "netral";
            if (clubsLastWeek == 0)
            {
                percent = 100;
            }
            else
            {
                percent = (int)(((double)clubsThisWeek / clubsLastWeek) * 100);
                if (percent > 100)
                {
                    percent = 100;
                }
            }
            if (clubsThisWeek - clubsLastWeek > 0)
                progress = "up";
            else if (clubsThisWeek - clubsLastWeek < 0)
                progress = "down";
            

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
                    usersList.Add(_userRepository.GetMany(u =>
                             u.Role == role &&
                              SqlFunctions.DatePart("week", u.LoggedAt) == thisWeek &&
                             u.CreateAt.Year == thisYear).Count());
                }
                else
                {
                    usersList.Add(_userRepository.GetMany(u =>
                             SqlFunctions.DatePart("week", u.LoggedAt) == thisWeek &&
                            u.CreateAt.Year == thisYear).Count());
                }
                thisWeek--;
            }
            usersList.Reverse();
            return usersList;

        }

    }
}
