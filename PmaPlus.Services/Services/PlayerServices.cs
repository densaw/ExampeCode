﻿using System;
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
        private readonly IAddressRepository _addressRepository;
        private readonly IUserDetailRepository _userDetailRepository;

        public PlayerServices(IPlayerRepository playerRepository, IActivityStatusChangeRepository activityStatusChangeRepository, ITeamRepository teamRepository, IUserRepository userRepository, IClubRepository clubRepository, IAddressRepository addressRepository, IUserDetailRepository userDetailRepository)
        {
            _playerRepository = playerRepository;
            _activityStatusChangeRepository = activityStatusChangeRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _clubRepository = clubRepository;
            _addressRepository = addressRepository;
            _userDetailRepository = userDetailRepository;
        }

        #region ClubPlayers


        public bool PlayerExist(int id)
        {
            return _playerRepository.GetMany(p => p.Id == id).Any();
        }
        public IEnumerable<PlayerTableViewModel> GetPlayersTable(int clubId)
        {
            return from player in _playerRepository.GetMany(p => p.Club.Id == clubId)
                   select new PlayerTableViewModel()
                   {
                       Id = player.Id,
                       Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                       Age = DateTime.Now.Year - (player.User.UserDetail.Birthday ?? DateTime.Now).Year,
                       ProfilePicture = player.User.UserDetail.ProfilePicture,
                       Teams = player.Teams.Select(t => t.Name)
                       //TODO:Finish player table
                   };
        }

        public IEnumerable<Player> GetClubPlayers(int clubId)
        {
            return _playerRepository.GetMany(p => p.Club.Id == clubId);
        }

        public IEnumerable<AvailablePlayersList> GetFreePlayers(int clubId)
        {
            return from player in _playerRepository.GetMany(p => p.Club.Id == clubId && p.Teams.Count < 2)
                   select new AvailablePlayersList()
                   {
                       Id = player.Id,
                       Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName
                   };
        }



        public Player AddPlayer(AddPlayerViewModel playerViewModel, int clubId)
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


            return _playerRepository.Add(player);
        }

        public AddPlayerViewModel GetPlayerViewModel(int playerId)
        {
            var player = _playerRepository.GetById(playerId);

            return new AddPlayerViewModel()
            {
                FirstName = player.User.UserDetail.FirstName,
                LastName = player.User.UserDetail.LastName,
                UserStatus = player.Status,
                Email = player.User.Email,
                Password = player.User.Password,
                Telephone = player.User.UserDetail.Address.Telephone,
                Mobile = player.User.UserDetail.Address.Mobile,
                FaNumber = player.User.UserDetail.FaNumber,
                BirthDate = player.User.UserDetail.Birthday,
                PlayingFoot = player.PlayingFoot,
                ProfilePicture = player.User.UserDetail.ProfilePicture,
                Nationality = player.User.UserDetail.Nationality,
                Address1 = player.User.UserDetail.Address.Address1,
                Address2 = player.User.UserDetail.Address.Address2,
                Address3 = player.User.UserDetail.Address.Address3,
                TownCity = player.User.UserDetail.Address.TownCity,
                Postcode = player.User.UserDetail.Address.PostCode,
                ParentsContactNumber = player.ParentsContactNumber,
                ParentsFirstName = player.ParentsFirstName,
                ParentsLastName = player.ParentsLastName,
                PlayerHealthConditions = player.PlayerHealthConditions,
                SchoolName = player.SchoolName,
                SchoolAddress1 = player.SchoolAddress1,
                SchoolAddress2 = player.SchoolAddress2,
                SchoolContactEmail = player.SchoolContactEmail,
                SchoolContactName = player.SchoolContactName,
                SchoolPostcode = player.SchoolPostcode,
                SchoolTelephone = player.SchoolTelephone,
                SchoolTownCity = player.SchoolTownCity,
            };

        }
        public void UpdatePlayer(AddPlayerViewModel playerViewModel, int playerId)
        {
            //Cut teams count
            playerViewModel.Teams = playerViewModel.Teams.Take(2).ToList();

            var player = _playerRepository.GetById(playerId);

            player.User.UserDetail.FirstName = playerViewModel.FirstName;
            player.User.UserDetail.LastName = playerViewModel.LastName;
            //teams
            foreach (var team in playerViewModel.Teams)
            {
                if (!player.Teams.Any(t => t.Id == team))
                {
                    player.Teams.Add(_teamRepository.GetById(team));
                }
            }
            foreach (var team in player.Teams)
            {
                if (!playerViewModel.Teams.Contains(team.Id))
                {
                    player.Teams.Remove(team);
                }
            }


            player.Status = playerViewModel.UserStatus;
            player.User.Email = playerViewModel.Email;
            player.User.UserName = playerViewModel.Email;
            player.User.Password = playerViewModel.Password;
            player.User.UserDetail.Address.Telephone = playerViewModel.Telephone;
            player.User.UserDetail.Address.Mobile = playerViewModel.Mobile;
            player.User.UserDetail.FaNumber = playerViewModel.FaNumber;
            player.User.UserDetail.Birthday = playerViewModel.BirthDate;
            player.PlayingFoot = playerViewModel.PlayingFoot;
            player.User.UserDetail.ProfilePicture = playerViewModel.ProfilePicture;
            player.User.UserDetail.Nationality = playerViewModel.Nationality;
            player.User.UserDetail.Address.Address1 = playerViewModel.Address1;
            player.User.UserDetail.Address.Address2 = playerViewModel.Address2;
            player.User.UserDetail.Address.Address3 = playerViewModel.Address3;
            player.User.UserDetail.Address.TownCity = playerViewModel.TownCity;
            player.User.UserDetail.Address.PostCode = playerViewModel.Postcode;

            player.ParentsContactNumber = playerViewModel.ParentsContactNumber;
            player.ParentsFirstName = playerViewModel.ParentsFirstName;
            player.ParentsLastName = playerViewModel.ParentsLastName;
            player.PlayerHealthConditions = playerViewModel.PlayerHealthConditions;
            player.SchoolName = playerViewModel.SchoolName;
            player.SchoolAddress1 = playerViewModel.SchoolAddress1;
            player.SchoolAddress2 = playerViewModel.SchoolAddress2;
            player.SchoolContactEmail = playerViewModel.SchoolContactEmail;
            player.SchoolContactName = playerViewModel.SchoolContactName;
            player.SchoolPostcode = playerViewModel.SchoolPostcode;
            player.SchoolTelephone = playerViewModel.SchoolTelephone;
            player.SchoolTownCity = playerViewModel.SchoolTownCity;

            _playerRepository.Update(player, player.Id);
        }

        public void UpdatePlayer(Player player)
        {
            _playerRepository.Update(player,player.Id);
        }
        public void DeletePlayer(int id)
        {
            var player = _playerRepository.GetById(id);
            if (player != null)
            {
                _addressRepository.Delete(player.User.UserDetail.Address);
                _userDetailRepository.Delete(player.User.UserDetail);
                _userRepository.Delete(player.User);
                _playerRepository.Delete(player);
                //TODO: Maybe diary delete
            }
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
