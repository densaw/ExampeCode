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
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Club;

namespace PmaPlus.Services
{
    public class ClubServices
    {
        private readonly IClubRepository _clubRepository;
        private readonly IWelfareOfficerRepository _welfareOfficerRepository;
        public ClubServices(IClubRepository clubRepository, IWelfareOfficerRepository welfareOfficerRepository)
        {
            _clubRepository = clubRepository;
            _welfareOfficerRepository = welfareOfficerRepository;
        }

        public IEnumerable<ClubTableViewModel> GetClubsTableViewModels()
        {
            var clubs = from club in _clubRepository.GetAll()
                        select new ClubTableViewModel()
                        {
                            Id = club.Id,
                            Name = club.Name,
                            TownCity = club.Address.TownCity,
                            Coaches = club.Coaches.Count,
                            Players = club.Players.Count,
                            PiPay = club.Players.Count(p => p.Status == PlayerStatus.Active),
                            LastLogin = club.ClubAdmin.User.LoggedAt,
                            Status = club.Status
                        };
            return clubs;
        }

        public AddClubViewModel GetClubById(int id)
        {
            Club club = _clubRepository.GetById(id);
            AddClubViewModel model = new AddClubViewModel()
            {
                Name = club.Name,
                Logo = club.Logo,
                Status = club.Status,
                ClubAdminName = club.ClubAdmin.User.UserDetail.FirstName,
                ClubAdminUsername = club.ClubAdmin.User.UserName,
                ClubAdminPassword = club.ClubAdmin.User.Password,
                Background = club.Background,
                Established = club.Established,
                Telephone = club.Address.Telephone,
                Mobile = club.Address.Mobile,
                Address1 = club.Address.Address1,
                Address2 = club.Address.Address2,
                Address3 = club.Address.Address3,
                TownCity = club.Address.TownCity,
                PostCode = club.Address.PostCode,
                Chairman = club.Chairman.Name,
                ChairmanEmail = club.Chairman.Email,
                ChairmanTelephone = club.Chairman.Telephone,
                WelfareOfficer = club.WelfareOfficer.User.UserDetail.FirstName,
                WelfareOfficerEmail = club.WelfareOfficer.User.Email,
                WelfareOfficerTelephone = club.WelfareOfficer.User.UserDetail.Address.Telephone
            };
            return model;
        }

        public void AddClub(AddClubViewModel club)
        {
            var entity = new Club()
            {
                Name = club.Name,
                Logo = club.Logo,
                Status = club.Status,
                ClubAdmin = new ClubAdmin()
                {
                    User = new User()
                    {
                        UserName = club.ClubAdminUsername,
                        Role = Role.ClubAdmin,
                        Email = club.ClubAdminUsername,
                        Password = club.ClubAdminPassword,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        LoggedAt = DateTime.Now,
                        UserDetail = new UserDetail()
                        {
                            FirstName = club.ClubAdminName
                        }
                    },
                },

                Background = club.Background,
                Established = club.Established,
                Address = new Address()
                {
                    Telephone = club.Telephone,
                    Mobile = club.Mobile,
                    Address1 = club.Address1,
                    Address2 = club.Address2,
                    Address3 = club.Address3,
                    TownCity = club.TownCity,
                    PostCode = club.PostCode,
                },
                Chairman = new Chairman()
                {
                    Name = club.Chairman,
                    Email = club.ChairmanEmail,
                    Telephone = club.ChairmanTelephone,
                },
                WelfareOfficer = _welfareOfficerRepository.Get(w => w.User.Email == club.WelfareOfficerEmail)
            };
            _clubRepository.Add(entity);
            var welfare = _welfareOfficerRepository.Get(w => w.User.Email == club.WelfareOfficerEmail);
            if (welfare != null)
            {
                welfare.Clubs.Add(entity);
                _welfareOfficerRepository.Update(welfare);
            }
        }
        public void UpdateClub(AddClubViewModel club,int id)
        {
            if (club.Id > 0)
            {
                var entity = _clubRepository.GetById(id);

                entity.Name = club.Name;
                entity.Logo = club.Logo;
                entity.Status = club.Status;
                entity.ClubAdmin.User.UserDetail.FirstName = club.ClubAdminName;
                entity.ClubAdmin.User.UserName = club.ClubAdminUsername;
                entity.ClubAdmin.User.Password = club.ClubAdminPassword;
                entity.Background = club.Background;
                entity.Established = club.Established;
                entity.Address.Telephone = club.Telephone;
                entity.Address.Mobile = club.Mobile;
                entity.Address.Address1 = club.Address1;
                entity.Address.Address2 = club.Address2;
                entity.Address.Address3 = club.Address3;
                entity.Address.TownCity = club.TownCity;
                entity.Address.PostCode = club.PostCode;
                entity.Chairman.Name = club.Chairman;
                entity.Chairman.Email = club.ChairmanEmail;
                entity.Chairman.Telephone = club.ChairmanTelephone;
                entity.WelfareOfficer.User.UserDetail.FirstName = club.WelfareOfficer;
                entity.WelfareOfficer.User.Email = club.WelfareOfficerEmail;
                entity.WelfareOfficer.User.UserDetail.Address.Telephone = club.WelfareOfficerTelephone;

                _clubRepository.Update(entity);
            }
           
        }

        public void DeleteClub(int id)
        {
            _clubRepository.Delete(c => c.Id == id);
        }
        public InfoBoxViewModel GetClubLoggedThisWeek()
        {
            int clubsThisWeek =
                _clubRepository.GetAll().ToList().Where(c => DateTool.GetWeekNumber(c.CreateAt) == DateTool.GetThisWeek()).Count();
            int lastWeek = DateTool.GetThisWeek() > 1 ? DateTool.GetThisWeek() - 1 : 52;
            int clubsLastWeek =
                _clubRepository.GetAll().ToList().Where(c => DateTool.GetWeekNumber(c.CreateAt) == lastWeek).Count();
            int percent;
            string progress = "netral";
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
