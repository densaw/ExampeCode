using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Enums;

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

        public int GetActivePlayers()
        {
            return _playerRepository.GetMany(p => p.User.Status == PlayerStatus.Active).Count();
        }


        public int GetActivePlayersForMonth(DateTime dateTime)
        {
            return
                _activityStatusChangeRepository.GetMany(
                    a => a.DateTime.Month == dateTime.Month && a.DateTime.Year == dateTime.Year).Count();
        }

        
    }
}
