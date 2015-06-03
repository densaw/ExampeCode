using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class TalentServices
    {
        private readonly ITalentIdentificationRepository _talentIdentificationRepository;

        public TalentServices(ITalentIdentificationRepository talentIdentificationRepository)
        {
            _talentIdentificationRepository = talentIdentificationRepository;
        }

        #region Talent identification


        public bool TalentExist(int id)
        {
            return _talentIdentificationRepository.GetMany(t => t.Id == id).Any();
        }

        public IEnumerable<TalentIdentification> GetTalentIdentifications(int clubId)
        {
            return _talentIdentificationRepository.GetMany(t => t.ClubId == clubId);
        }

        public TalentIdentification GetTalentIdentificationById(int Id)
        {
            return _talentIdentificationRepository.GetById(Id);
        }

        public TalentIdentification AddTalentIdentification(TalentIdentification talentIdentification, int clubId)
        {
            talentIdentification.ClubId = clubId;
            return _talentIdentificationRepository.Add(talentIdentification);
        }

        public void UpdateTalentIdentification(TalentIdentification talentIdentification, int id)
        {
            talentIdentification.Id = id;
            _talentIdentificationRepository.Update(talentIdentification, talentIdentification.Id);
        }

        public void DeleteIdentification(int id)
        {
            _talentIdentificationRepository.Delete(t => t.Id == id);
        }

        #endregion



    }
}
