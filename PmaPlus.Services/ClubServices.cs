using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data;
using PmaPlus.Data.Infrastructure;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.ViewModels;

namespace PmaPlus.Services
{
    public class ClubServices
    {
        private readonly IClubRepository _clubRepository;

        public ClubServices(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public InfoBoxViewModel GetClubLoggedThisWeek()
        {
            int clubsThisWeek =
                _clubRepository.GetAll().ToList().Where(c => DateTool.GetWeekNumber(c.CreateAt) == DateTool.GetThisWeek()).Count();
            int lastWeek = DateTool.GetThisWeek() > 1 ? DateTool.GetThisWeek() - 1 : 52;
            int clubsLastWeek =
                _clubRepository.GetAll().ToList().Where(c =>  DateTool.GetWeekNumber(c.CreateAt) == lastWeek).Count();
            return new InfoBoxViewModel()
            {
                Amount = clubsThisWeek,
                Progress = clubsThisWeek > clubsLastWeek
            };
        }


    }
}
