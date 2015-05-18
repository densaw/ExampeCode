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
using PmaPlus.Model.ViewModels.Player;

namespace PmaPlus.Services
{
    public class PlayerServices
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IActivityStatusChangeRepository _activityStatusChangeRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClubRepository _clubRepository;

        public PlayerServices(IPlayerRepository playerRepository, IActivityStatusChangeRepository activityStatusChangeRepository, ITeamRepository teamRepository, IUserRepository userRepository, IClubRepository clubRepository)
        {
            _playerRepository = playerRepository;
            _activityStatusChangeRepository = activityStatusChangeRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _clubRepository = clubRepository;
        }

        #region ClubPlayers

        public IEnumerable<PlayerTableViewModel> GetPlayersTable(int clubId)
        {
            return from player in _playerRepository.GetMany(p => p.Club.Id == clubId)
                select new PlayerTableViewModel()
                {
                    Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                    Age = DateTime.Now.Year - (player.User.UserDetail.Birthday ?? DateTime.Now).Year,
                    Teams = player.Teams.Select(t => t.Name).AsEnumerable()
                    //TODO:Finish player table
                };
        }

        public IEnumerable<Player> GetClubPlayers(int clubId)
        {
            return _playerRepository.GetMany(p => p.Club.Id == clubId);
        }

        public IEnumerable<Player> GetFreePlayers(int clubId)
        {
            return _playerRepository.GetMany(p => p.Club.Id == clubId && p.Teams.Count < 2);
        }

        public User AddPlayer(AddPlayerViewModel playerViewModel, int clubId)
        {
            var user = new User()
            {
                UserName = playerViewModel.Email,
                Email = playerViewModel.Email,
                Password = playerViewModel.Password,
                Role = Role.Player,
                CreateAt = DateTime.Now,
                LoggedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                UserDetail = new UserDetail()
                {
                    FirstName = playerViewModel.FirstName,
                    LastName = playerViewModel.LastName,
                    Birthday = playerViewModel.BirthDate,
                    FaNumber = playerViewModel.FaNumber,
                    Nationality = playerViewModel.Nationality,
                    ProfilePicture = playerViewModel.ProfilePicture,
                    Address = new Address()
                    {
                        Address1 = playerViewModel.Address1,
                        Address2 = playerViewModel.Address2,
                        Address3 = playerViewModel.Address3,
                        Telephone = playerViewModel.Telephone,
                        Mobile = playerViewModel.Mobile,
                        TownCity = playerViewModel.TownCity,
                        PostCode = playerViewModel.Postcode
                    }
                }
            };

            var newUser = _userRepository.Add(user);

            var player = new Player()
            {
                Status = UserStatus.Active,
                PlayingFoot = playerViewModel.PlayingFoot,
                User = newUser,
                Club = _clubRepository.GetById(clubId),
                ParentsContactNumber = playerViewModel.ParentsContactNumber,
                ParentsFirstName = playerViewModel.ParentsFirstName,
                ParentsLastName = playerViewModel.ParentsLastName,
                PlayerHealthConditions = playerViewModel.PlayerHealthConditions,
                SchoolName = playerViewModel.SchoolName,
                SchoolAddress1 = playerViewModel.SchoolAddress1,
                SchoolAddress2 = playerViewModel.SchoolAddress2,
                SchoolContactEmail = playerViewModel.SchoolContactEmail,
                SchoolContactName = playerViewModel.SchoolContactName,
                SchoolPostcode = playerViewModel.SchoolPostcode,
                SchoolTelephone = playerViewModel.SchoolTelephone,
                SchoolTownCity = playerViewModel.SchoolTownCity,
            };

            var userTeams = _teamRepository.GetMany(t => playerViewModel.Teams.Contains(t.Id));

            foreach (var team in userTeams)
            {
                player.Teams.Add(team);
            }
            _playerRepository.Add(player);

            return newUser;
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
