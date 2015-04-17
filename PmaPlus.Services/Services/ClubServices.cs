using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data;
using PmaPlus.Data.Infrastructure;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Enums;
using PmaPlus.Model.ViewModels;

namespace PmaPlus.Services
{
    public class ClubServices
    {
        private readonly IClubRepository _clubRepository;
        public ClubServices(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;

            //_clubRepository.Get(u => u.Id == 0).ClubAdmin.User.UserDetail.FirstName;
        }


        
        public class ClubViewModel
        {
            public string Name { get; set; }
            public string Logo { get; set; }
            public ClubStatus Status { get; set; }
        }
        
        
        

        public InfoBoxViewModel GetClubLoggedThisWeek()
        {
            int clubsThisWeek =
                _clubRepository.GetAll().ToList().Where(c => DateTool.GetWeekNumber(c.CreateAt) == DateTool.GetThisWeek()).Count();
            int lastWeek = DateTool.GetThisWeek() > 1 ? DateTool.GetThisWeek() - 1 : 52;
            int clubsLastWeek =
                _clubRepository.GetAll().ToList().Where(c =>  DateTool.GetWeekNumber(c.CreateAt) == lastWeek).Count();
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

        public IList<int> GetClubsLoggedForLast_Weeks(int times = 10)
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

                usersList.Add(_clubRepository.GetAll().ToList().Where(c =>
                         DateTool.GetWeekNumber(c.CreateAt) == thisWeek &&
                         c.CreateAt.Year == thisYear).Count());
                thisWeek--;
            }
            usersList.Reverse();
            return usersList;

        } 
    }
}
