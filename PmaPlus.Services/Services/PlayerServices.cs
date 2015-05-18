using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;

namespace PmaPlus.Services
{
    public class PlayerServices
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IActivityStatusChangeRepository _activityStatusChangeRepository;

        public PlayerServices(IPlayerRepository playerRepository, IActivityStatusChangeRepository activityStatusChangeRepository)
        {
            _playerRepository = playerRepository;
            _activityStatusChangeRepository = activityStatusChangeRepository;
        }

        #region ClubPlayers

        public IEnumerable<Player> GetClubPlayers(int clubId)
        {
            return _playerRepository.GetMany(p => p.Club.Id == clubId);
        } 

        public IEnumerable<Player> GetFreePlayers(int clubId)
        {
            return _playerRepository.GetMany(p => p.Club.Id == clubId && p.Teams.Count < 2);
        }




        #endregion

        public int GetActivePlayers()
        {
            return _playerRepository.GetMany(p => p.Status == UserStatus.Active).Count();
        }

  

        public int GetActivePlayersForMonth(DateTime dateTime)
        {
            return
                _activityStatusChangeRepository.GetMany(
                    a => a.DateTime.Month == dateTime.Month && a.DateTime.Year == dateTime.Year).Count();
        }

        public List<ActivePlayersForLastYearViewModel> GetActivePlayersForLastYear()
        {
            List<DateTime> dates = new List<DateTime>();

            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            for (int i = 12 - 1; i >= 0; i--)
            {
                if (month < 1)
                {
                    month = 12;
                    year--;
                }
                dates.Add(new DateTime(year, month, 1));
                month--;
            }
            dates.Reverse();
        
            List<ActivePlayersForLastYearViewModel> activePlayers = new List<ActivePlayersForLastYearViewModel>();

            foreach (var date in dates)
            {
                activePlayers.Add(new ActivePlayersForLastYearViewModel()
                {
                    ActivePlayers = GetActivePlayersForMonth(date),
                    Month = date.Month
                });
            }
            return activePlayers;
        } 
    }
}
