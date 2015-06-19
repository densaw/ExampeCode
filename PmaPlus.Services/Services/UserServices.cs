using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IAddressRepository _addressRepository;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IPlayerRepository _playerRepository;

        public UserServices(IUserRepository userRepository, IClubAdminRepository clubAdminRepository,
            ICoachRepository coachRepository, IClubRepository clubRepository,
            IWelfareOfficerRepository welfareOfficerRepository, IScoutRepository scoutRepository,
            IPhysiotherapistRepository physiotherapistRepository, IHeadOfEducationRepository headOfEducationRepository,
            IHeadOfAcademyRepository headOfAcademyRepository, ISportScientistRepository sportScientistRepository,
            IAddressRepository addressRepository, IUserDetailRepository userDetailRepository, IPlayerRepository playerRepository)
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
            _addressRepository = addressRepository;
            _userDetailRepository = userDetailRepository;
            _playerRepository = playerRepository;
        }

        public ClubAdmin GetClubAdminByUserName(string name)
        {
            return _clubAdminRepository.Get(a => a.User.UserName == name);
        }

        

        public Club GetClubByUserName(string name)
        {
            var user = _userRepository.Get(u => u.Email.ToLower() == name.ToLower());
            if (user != null)
            {
                switch (user.Role)
                {
                    case Role.Player:
                        {
                            var member = _playerRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;

                            break;
                        }
                    case Role.ClubAdmin:
                        {
                            var member = _clubAdminRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;

                            break;
                        }

                    case Role.Coach:
                        {
                            var member = _coachRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;

                            break;
                        }
                    case Role.HeadOfAcademies:
                        {
                            var member = _headOfAcademyRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;
                            break;
                        }
                    case Role.HeadOfEducation:
                        {
                            var member = _headOfEducationRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;
                            break;
                        }
                    case Role.Scout:
                        {
                            var member = _scoutRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;
                            break;
                        }
                    case Role.Physiotherapist:
                        {
                            var member = _physiotherapistRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;
                            break;
                        }
                    case Role.SportsScientist:
                        {
                            var member = _sportScientistRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;
                            break;
                        }
                    case Role.WelfareOfficer:
                        {
                            var member = _welfareOfficerRepository.Get(c => c.User.Id == user.Id);
                            if (member != null)
                                return member.Club;
                            break;
                        }
                    default:
                        {
                            return null;
                        }
                }


            }
            return null;
        }


        public IEnumerable<User> GetUsersByRoles(IList<Role> roles)
        {
            List<User> userList = new List<User>();
            foreach (var role in roles)
            {
                userList.AddRange(_userRepository.GetMany(u => u.Role == role));
            }
            return userList;
        }

        public IEnumerable<User> GetUsersByRolesWithOutItSelf(IList<Role> roles, int userId)
        {
            List<User> userList = new List<User>();
            foreach (var role in roles)
            {
                userList.AddRange(_userRepository.GetMany(u => u.Role == role).Where(x => x.Id != userId));
            }
            return userList;
        }

        public bool UserExist(int id)
        {
            return _userRepository.GetMany(u => u.Id == id).Any();
        }

        public bool UserExist(string userEmail)
        {
            return _userRepository.GetMany(u => u.Email.ToLower() == userEmail.ToLower()).Any();
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.Get(u => u.Email.ToLower() == email.ToLower());
        }

        #region TeamMembers


        public IEnumerable<TrainingTeamMemberPlateViewModel> GetTrainingTeamMembers(int clubId, string name)
        {
            List<User> userList = new List<User>();

            userList.AddRange(_headOfAcademyRepository.GetMany(h => h.Club.Id == clubId && h.User.Email.ToLower() != name.ToLower()).Select(h => h.User));
            userList.AddRange(_headOfEducationRepository.GetMany(h => h.Club.Id == clubId).Select(h => h.User));
            userList.AddRange(_coachRepository.GetMany(h => h.Club.Id == clubId).Select(h => h.User));
            userList.AddRange(_physiotherapistRepository.GetMany(h => h.Club.Id == clubId).Select(h => h.User));
            userList.AddRange(_scoutRepository.GetMany(h => h.Club.Id == clubId).Select(h => h.User));
            userList.AddRange(_sportScientistRepository.GetMany(h => h.Club.Id == clubId).Select(h => h.User));
            userList.AddRange(_welfareOfficerRepository.GetMany(h => h.Club.Id == clubId).Select(h => h.User));


            var trTeamMember = from user in userList

                               select new TrainingTeamMemberPlateViewModel()
                               {
                                   Id = user.Id,
                                   Name = user.UserDetail.FirstName + " " + user.UserDetail.LastName,
                                   Role = user.Role,
                                   Email = user.Email,
                                   TownCity = user.UserDetail.Address.TownCity,
                                   Mobile = user.UserDetail.Address.Mobile,
                                   LastLogin = user.LoggedAt,
                                   ProfilePicture = user.UserDetail.ProfilePicture,
                                   AboutMe = user.UserDetail.AboutMe,
                                   BirthDay = user.UserDetail.Birthday,
                                   CrbDbsExpiry = user.UserDetail.CrbDbsExpiry,
                                   FirstAidExpiry = user.UserDetail.FirstAidExpiry,
                                   Age = DateTime.Now.Year - (user.UserDetail.Birthday ?? DateTime.Now).Year
                               };

            return trTeamMember;
        }

        public User AddTrainingTeamMember(AddTrainingTeamMemberViewModel user, string clubAdminEmail)
        {
            var club = GetClubByUserName(clubAdminEmail);

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
                    FirstAidExpiry = user.FirstAidExpiry,

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
            newUser.UserDetail.User = newUser;
            switch (newUser.Role)
            {
                case Role.Coach:
                    {
                        var newCoach = new Coach()
                        {
                            User = newUser,
                            Status = UserStatus.Active,
                            Club = club
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
                            Club = club
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
                            Club = club
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
                            Club = club
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
                            Club = club
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
                            Club = club
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
                            Club = club
                        };
                        _welfareOfficerRepository.Add(welfareOfficer);
                        break;
                    }

            }
            _userRepository.Update(newUser, newUser.Id);
            return newUser;
        }

        public TrainingTeamMemberViewModel GetTrainingTeamMemberViewModel(int id)
        {
            var trTeamMember = _userRepository.Get(u => u.Id == id);
            return new TrainingTeamMemberViewModel()
            {
                Id = trTeamMember.Id,
                Name = trTeamMember.UserDetail.FirstName + " " + trTeamMember.UserDetail.LastName,
                TownCity = trTeamMember.UserDetail.Address.TownCity,
                PostCode = trTeamMember.UserDetail.Address.PostCode,
                Mobile = trTeamMember.UserDetail.Address.Mobile,
                AboutMe = trTeamMember.UserDetail.AboutMe,
                ProfilePicture = trTeamMember.UserDetail.ProfilePicture
            };
        }

        public AddTrainingTeamMemberViewModel GetDetailedTrainingTeamMemberViewModel(int id)
        {
            var trTeamMember = _userRepository.Get(u => u.Id == id);
            var member = new AddTrainingTeamMemberViewModel()
            {
                Id = trTeamMember.Id,
                Role = trTeamMember.Role,
                FirstName = trTeamMember.UserDetail.FirstName,
                LastName = trTeamMember.UserDetail.LastName,
                AboutMe = trTeamMember.UserDetail.AboutMe,
                Telephone = trTeamMember.UserDetail.Address.Telephone,
                Mobile = trTeamMember.UserDetail.Address.Mobile,
                Email = trTeamMember.Email,
                Password = trTeamMember.Password,
                FaNumber = trTeamMember.UserDetail.FaNumber,
                BirthDate = trTeamMember.UserDetail.Birthday,
                ProfilePicture = trTeamMember.UserDetail.ProfilePicture,
                Nationality = trTeamMember.UserDetail.Nationality,
                Address1 = trTeamMember.UserDetail.Address.Address1,
                Address2 = trTeamMember.UserDetail.Address.Address2,
                Address3 = trTeamMember.UserDetail.Address.Address3,
                Postcode = trTeamMember.UserDetail.Address.PostCode,
                CrbDbsExpiry = trTeamMember.UserDetail.CrbDbsExpiry,
                FirstAidExpiry = trTeamMember.UserDetail.CrbDbsExpiry,
                TownCity = trTeamMember.UserDetail.Address.TownCity,
            };

            switch (trTeamMember.Role)
            {
                case Role.Coach:
                    {
                        member.UserStatus = _coachRepository.Get(c => c.User.Id == id).Status;
                        break;
                    }
                case Role.HeadOfAcademies:
                    {
                        member.UserStatus = _headOfAcademyRepository.Get(c => c.User.Id == id).Status;
                        ;
                        break;
                    }
                case Role.HeadOfEducation:
                    {
                        member.UserStatus = _headOfEducationRepository.Get(c => c.User.Id == id).Status;
                        ;
                        break;
                    }
                case Role.Scout:
                    {
                        member.UserStatus = _scoutRepository.Get(c => c.User.Id == id).Status;
                        ;
                        break;
                    }
                case Role.Physiotherapist:
                    {
                        member.UserStatus = _physiotherapistRepository.Get(c => c.User.Id == id).Status;
                        ;
                        break;
                    }
                case Role.SportsScientist:
                    {
                        member.UserStatus = _sportScientistRepository.Get(c => c.User.Id == id).Status;
                        break;
                    }
                case Role.WelfareOfficer:
                    {
                        member.UserStatus = _welfareOfficerRepository.Get(c => c.User.Id == id).Status;
                        break;
                    }

            }

            return member;
        }

        public void UpdateTrainigTeamMember(AddTrainingTeamMemberViewModel memberViewModel, int id)
        {

            var member = _userRepository.Get(u => u.Id == id);
            if (member != null)
            {
                member.UserDetail.FirstName = memberViewModel.FirstName;
                member.UserDetail.LastName = memberViewModel.LastName;
                member.UserDetail.AboutMe = memberViewModel.AboutMe;
                member.UserDetail.Address.Telephone = memberViewModel.Telephone;
                member.UserDetail.Address.Mobile = memberViewModel.Mobile;
                member.Email = memberViewModel.Email;
                member.UserName = memberViewModel.Email;
                member.Password = memberViewModel.Password;
                member.UserDetail.FaNumber = memberViewModel.FaNumber;
                member.UserDetail.Birthday = memberViewModel.BirthDate;
                member.UserDetail.ProfilePicture = memberViewModel.ProfilePicture;
                member.UserDetail.Nationality = memberViewModel.Nationality;
                member.UserDetail.Address.Address1 = memberViewModel.Address1;
                member.UserDetail.Address.Address2 = memberViewModel.Address2;
                member.UserDetail.Address.Address3 = memberViewModel.Address3;
                member.UserDetail.Address.PostCode = memberViewModel.Postcode;
                member.UserDetail.Address.TownCity = memberViewModel.TownCity;
                member.UserDetail.CrbDbsExpiry = memberViewModel.CrbDbsExpiry;
                member.UserDetail.FirstAidExpiry = memberViewModel.FirstAidExpiry;


                _userRepository.Update(member, member.Id);

                switch (member.Role)
                {
                    case Role.Coach:
                        {
                            var coach = _coachRepository.Get(c => c.User.Id == id);
                            if (coach != null)
                            {
                                coach.Status = memberViewModel.UserStatus;
                                _coachRepository.Update(coach, coach.Id);
                            }

                            break;
                        }
                    case Role.HeadOfAcademies:
                        {
                            var headofa = _headOfAcademyRepository.Get(c => c.User.Id == id);
                            if (headofa != null)
                            {
                                headofa.Status = memberViewModel.UserStatus;
                                _headOfAcademyRepository.Update(headofa, headofa.Id);
                            }
                            break;
                        }
                    case Role.HeadOfEducation:
                        {
                            var headofe = _headOfEducationRepository.Get(c => c.User.Id == id);
                            if (headofe != null)
                            {
                                headofe.Status = memberViewModel.UserStatus;
                                _headOfEducationRepository.Update(headofe, headofe.Id);
                            }
                            break;
                        }
                    case Role.Scout:
                        {
                            var scout = _scoutRepository.Get(c => c.User.Id == id);
                            if (scout != null)
                            {
                                scout.Status = memberViewModel.UserStatus;
                                _scoutRepository.Update(scout, scout.Id);
                            }
                            break;
                        }
                    case Role.Physiotherapist:
                        {
                            var terapist = _physiotherapistRepository.Get(c => c.User.Id == id);
                            if (terapist != null)
                            {
                                terapist.Status = memberViewModel.UserStatus;
                                _physiotherapistRepository.Update(terapist, terapist.Id);
                            }
                            break;
                        }
                    case Role.SportsScientist:
                        {
                            var scientist = _sportScientistRepository.Get(c => c.User.Id == id);
                            if (scientist != null)
                            {
                                scientist.Status = memberViewModel.UserStatus;
                                _sportScientistRepository.Update(scientist, scientist.Id);
                            }
                            break;
                        }
                    case Role.WelfareOfficer:
                        {
                            var welfare = _welfareOfficerRepository.Get(c => c.User.Id == id);
                            if (welfare != null)
                            {
                                welfare.Status = memberViewModel.UserStatus;
                                _welfareOfficerRepository.Update(welfare, welfare.Id);
                            }
                            break;
                        }

                }
            }

        }


        public void DeleteTrainigTeamMember(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                _addressRepository.Delete(user.UserDetail.Address);
                _userDetailRepository.Delete(user.UserDetail);

                switch (user.Role)
                {
                    case Role.Coach:
                        {
                            _coachRepository.Delete(c => c.User.Id == user.Id);
                            break;
                        }
                    case Role.HeadOfAcademies:
                        {
                            _headOfAcademyRepository.Delete(c => c.User.Id == user.Id);
                            break;
                        }
                    case Role.HeadOfEducation:
                        {
                            _headOfEducationRepository.Delete(c => c.User.Id == user.Id);
                            break;
                        }
                    case Role.Scout:
                        {
                            _scoutRepository.Delete(c => c.User.Id == user.Id);
                            break;
                        }
                    case Role.Physiotherapist:
                        {
                            _physiotherapistRepository.Delete(c => c.User.Id == user.Id);
                            break;
                        }
                    case Role.SportsScientist:
                        {
                            _sportScientistRepository.Delete(c => c.User.Id == user.Id);
                            break;
                        }
                    case Role.WelfareOfficer:
                        {
                            _welfareOfficerRepository.Delete(c => c.User.Id == user.Id);
                            break;
                        }
                }

                _userRepository.Delete(user);
            }
        }

        #endregion

        public void UpdateUser(User user)
        {
            _userRepository.Update(user, user.Id);
        }

        public Coach GetCoachByUserName(string name)
        {
            return _coachRepository.Get(c => c.User.UserName.ToLower() == name.ToLower());
        }

        public string GetClubColorByUser(string email)
        {
            var user = GetUserByEmail(email);

            if (user == null)
            {
                return "#3276b1";
            }



            switch (user.Role)
            {

                case Role.ClubAdmin:
                    {
                        var coach = _clubAdminRepository.Get(c => c.User.Id == user.Id);
                        if (coach != null)
                        {
                            return coach.Club.ColorTheme;
                        }

                        break;
                    }

                case Role.Coach:
                    {
                        var coach = _coachRepository.Get(c => c.User.Id == user.Id);
                        if (coach != null)
                        {
                            return coach.Club.ColorTheme;
                        }

                        break;
                    }
                case Role.HeadOfAcademies:
                    {
                        var headofa = _headOfAcademyRepository.Get(c => c.User.Id == user.Id);
                        if (headofa != null)
                        {
                            return headofa.Club.ColorTheme;
                        }
                        break;
                    }
                case Role.HeadOfEducation:
                    {
                        var headofe = _headOfEducationRepository.Get(c => c.User.Id == user.Id);
                        if (headofe != null)
                        {
                            return headofe.Club.ColorTheme;
                        }
                        break;
                    }
                case Role.Scout:
                    {
                        var scout = _scoutRepository.Get(c => c.User.Id == user.Id);
                        if (scout != null)
                        {
                            return scout.Club.ColorTheme;
                        }
                        break;
                    }
                case Role.Physiotherapist:
                    {
                        var terapist = _physiotherapistRepository.Get(c => c.User.Id == user.Id);
                        if (terapist != null)
                        {
                            return terapist.Club.ColorTheme;
                        }
                        break;
                    }
                case Role.SportsScientist:
                    {
                        var scientist = _sportScientistRepository.Get(c => c.User.Id == user.Id);
                        if (scientist != null)
                        {
                            return scientist.Club.ColorTheme;
                        }
                        break;
                    }
                case Role.WelfareOfficer:
                    {
                        var welfare = _welfareOfficerRepository.Get(c => c.User.Id == user.Id);
                        if (welfare != null)
                        {
                            return welfare.Club.ColorTheme;
                        }
                        break;
                    }

                case Role.Player:
                    {
                        var player = _playerRepository.Get(c => c.User.Id == user.Id);
                        if (player != null)
                        {
                            return player.Club.ColorTheme;
                        }
                        break;
                    }

            }

            return "#3276b1";
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


        #region Users List

        public IEnumerable<Scout> GetClubScouts(int clubId)
        {
            return _scoutRepository.GetMany(c => c.Club.Id == clubId);
        }

        #endregion

    }
}
