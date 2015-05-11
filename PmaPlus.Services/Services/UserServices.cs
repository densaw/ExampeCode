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
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.TrainingTeamMember;

namespace PmaPlus.Services
{
    public class UserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IClubAdminRepository _clubAdminRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IHeadOfAcademyRepository _headOfAcademyRepository;
        private readonly IHeadOfEducationRepository _headOfEducationRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IScoutRepository _scoutRepository;
        private readonly IWelfareOfficerRepository _welfareOfficerRepository;
        private readonly ISportScientistRepository _sportScientistRepository;

        public UserServices(IUserRepository userRepository, IClubAdminRepository clubAdminRepository, ICoachRepository coachRepository, IClubRepository clubRepository, IWelfareOfficerRepository welfareOfficerRepository, IScoutRepository scoutRepository, IPhysiotherapistRepository physiotherapistRepository, IHeadOfEducationRepository headOfEducationRepository, IHeadOfAcademyRepository headOfAcademyRepository, ISportScientistRepository sportScientistRepository)
        {
            _userRepository = userRepository;
            _clubAdminRepository = clubAdminRepository;
            _coachRepository = coachRepository;
            _clubRepository = clubRepository;
            _welfareOfficerRepository = welfareOfficerRepository;
            _scoutRepository = scoutRepository;
            _physiotherapistRepository = physiotherapistRepository;
            _headOfEducationRepository = headOfEducationRepository;
            _headOfAcademyRepository = headOfAcademyRepository;
            _sportScientistRepository = sportScientistRepository;
        }

        public ClubAdmin GetClubAdminByUserName(string name)
        {
            return _clubAdminRepository.Get(a => a.User.UserName == name);
        }

        public bool UserExist(string userEmail)
        {
            return _userRepository.GetMany(u => u.Email.ToLower() == userEmail.ToLower()).Any();
        }
        public User GetUserByEmail(string email)
        {
            return _userRepository.Get(u => u.Email == email);
        }

        public IEnumerable<TrainingTeamMemberViewModel> GetTrainingTeamMembers()
        {
            var users = _userRepository.GetMany(u => u.Role != Role.ClubAdmin && u.Role != Role.SystemAdmin && u.Role != Role.Player);

            var trTeamMember = new List<TrainingTeamMemberViewModel>();

            foreach (var user in users)
            {
                trTeamMember.Add(new TrainingTeamMemberViewModel()
                {
                    Name = user.UserDetail.FirstName + user.UserDetail.LastName,
                    Role = user.Role,
                    TownCity = user.UserDetail.Address.TownCity,
                    BirthDay = user.UserDetail.Birthday.Value,
                    Age = DateTime.Now.Year - user.UserDetail.Birthday.Value.Year,
                    Mobile = user.UserDetail.Address.Mobile,
                    CrbDbsExpiry = user.UserDetail.CrbDbsExpiry.Value,
                    FirstAidExpiry = user.UserDetail.FirstAidExpiry.Value,
                    LastLogin = user.LoggedAt,
                    ProfilePicture = user.UserDetail.ProfilePicture

                });
            }

            return trTeamMember;
        }

        public User AddTrainingTeamMember(AddTrainingTeamMemberViewModel user, string clubAdminEmail)
        {
            var trTeam = new User
            {
                UserDetail = new UserDetail
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AboutMe = user.AboutMe,
                    Address = new Address()
                    {
                        Address1 = user.Address1,
                        Address2 = user.Address2,
                        Address3 = user.Address3,
                        Telephone = user.Telephone,
                        Mobile = user.Mobile,
                        TownCity = user.TownCity,
                        PostCode = user.Postcode
                    },
                    Birthday = user.BirthDate,
                    FaNumber = user.FaNumber,
                    Nationality = user.Nationality,
                    ProfilePicture = user.ProfilePicture,
                    CrbDbsExpiry = user.CrbDbsExpiry,
                    FirstAidExpiry = user.FirstAidExpiry

                },
                Role = user.Role,
                CreateAt = DateTime.Now,
                LoggedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Email = user.Email,
                Password = user.Password,
                UserName = user.Email,

            };
            var newUser = _userRepository.Add(trTeam);
            switch (newUser.Role)
            {
                case Role.Coach:
                    {
                        var newCoach = new Coach()
                        {
                            User = newUser,
                            Status = UserStatus.Active,
                            Club = _clubRepository.Get(c => c.ClubAdmin.User.Email.ToLower() == clubAdminEmail.ToLower()),
                        };
                        _coachRepository.Add(newCoach);
                        break;
                    }
                case Role.HeadOfAcademies:
                    {
                        var newHeadofA = new HeadOfAcademy()
                        {
                            User = newUser,
                            Status = UserStatus.Active,
                            Club = _clubRepository.Get(c => c.ClubAdmin.User.Email.ToLower() == clubAdminEmail.ToLower()),
                        };
                        _headOfAcademyRepository.Add(newHeadofA);
                        break;
                    }
                case Role.HeadOfEducation:
                    {
                        var newHeadofE = new HeadOfEducation()
                        {
                            User = newUser,
                            Status = UserStatus.Active,
                            Club = _clubRepository.Get(c => c.ClubAdmin.User.Email.ToLower() == clubAdminEmail.ToLower()),
                        };
                        _headOfEducationRepository.Add(newHeadofE);
                        break;
                    }
                case Role.Scout:
                    {
                        var scout = new Scout()
                        {
                            User = newUser,
                            Status = UserStatus.Active,
                            Club = _clubRepository.Get(c => c.ClubAdmin.User.Email.ToLower() == clubAdminEmail.ToLower()),
                        };
                        _scoutRepository.Add(scout);
                        break;
                    }
                case Role.Physiotherapist:
                    {
                        var physiotherapist = new Physiotherapist()
                        {
                            User = newUser,
                            Status = UserStatus.Active,
                            Club = _clubRepository.Get(c => c.ClubAdmin.User.Email.ToLower() == clubAdminEmail.ToLower()),
                        };
                        _physiotherapistRepository.Add(physiotherapist);
                        break;
                    }
                case Role.SportsScientist:
                    {
                        var sportScientist = new SportScientist()
                        {
                            User = newUser,
                            Status = UserStatus.Active,
                            Club = _clubRepository.Get(c => c.ClubAdmin.User.Email.ToLower() == clubAdminEmail.ToLower()),
                        };
                        _sportScientistRepository.Add(sportScientist);
                        break;
                    }
                case Role.WelfareOfficer:
                    {
                        var welfareOfficer = new WelfareOfficer()
                        {
                            User = newUser,
                            Status = UserStatus.Active,
                            Club = _clubRepository.Get(c => c.ClubAdmin.User.Email.ToLower() == clubAdminEmail.ToLower()),
                        };
                        _welfareOfficerRepository.Add(welfareOfficer);
                        break;
                    }

            }
            _userRepository.Update(newUser,newUser.Id);
            return newUser;
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user, user.Id);
        }

        public InfoBoxViewModel GetUsersLoggedThisWeek(Role role = 0)
        {
            int clubsThisWeek, clubsLastWeek;

            int thisWeek = DateTool.GetThisWeek();
            int lastWeek = DateTool.GetThisWeek() > 1 ? DateTool.GetThisWeek() - 1 : 52;
            if (role != 0)
            {

                clubsThisWeek =
                    _userRepository.GetMany(u => u.Role == role && SqlFunctions.DatePart("week", u.LoggedAt) == thisWeek).Count();
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
