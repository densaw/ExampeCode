using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class TrainingTeamMembersServices
    {
        private readonly UserServices _userServices;
        private readonly IUserRepository _userRepository;
        private readonly IQualificationRepository _qualificationRepository;
        private readonly IQualificationToFaCourseRepository _qualificationToFaCourseRepository;
        private readonly IFACourseRepository _faCourseRepository;

        public TrainingTeamMembersServices(UserServices userServices, IQualificationToFaCourseRepository qualificationToFaCourseRepository, IQualificationRepository qualificationRepository, IUserRepository userRepository, IFACourseRepository faCourseRepository)
        {
            _userServices = userServices;
            _qualificationToFaCourseRepository = qualificationToFaCourseRepository;
            _qualificationRepository = qualificationRepository;
            _userRepository = userRepository;
            _faCourseRepository = faCourseRepository;
        }

        #region Qualifications

        public IEnumerable<Qualification> GetTrainingTeamMemberQualifications( int userId)
        {
            return _qualificationRepository.GetMany(q => q.User.Id == userId);
        }


        public Qualification GetQualificationById(int qualId)
        {
            return _qualificationRepository.GetById(qualId);
        }
        public void AddQualificationForTeamMember(Qualification qualification, int userId)
        {
            var user = _userRepository.GetById(userId);
            qualification.User = user;
            var newQual = _qualificationRepository.Add(qualification);
            if (newQual.Type == CertificateCourseType.FA)
            {
                _qualificationToFaCourseRepository.Add(new QualificationToFaCourse()
                {
                    FaCourse = _faCourseRepository.Get(f=>f.Name.ToLower() == newQual.Name.ToLower()),
                    Qualification = newQual
                });
            }
        }

        public void UpdateQualification(Qualification qualification, int id)
        {
            qualification.Id = id;
            _qualificationRepository.Update(qualification,qualification.Id);

            if (qualification.Type == CertificateCourseType.FA)
            {
                var faC =
                    _qualificationToFaCourseRepository.Get(
                        q => q.FaCourse.Name.ToLower() == qualification.Name.ToLower());
                if (faC == null)
                {
                    _qualificationToFaCourseRepository.Add(new QualificationToFaCourse()
                    {
                        FaCourse = _faCourseRepository.Get(f => f.Name.ToLower() == qualification.Name.ToLower()),
                        Qualification = qualification
                    });
                }
            }
            else
            {
                _qualificationToFaCourseRepository.Delete(q=>q.Qualification.Id == qualification.Id);
            }
        }

        public void DeleteQualification(int id)
        {
            _qualificationToFaCourseRepository.Delete(q=>q.Qualification.Id == id);
            _qualificationRepository.Delete(q=>q.Id == id);

        }
        #endregion




    }
}
