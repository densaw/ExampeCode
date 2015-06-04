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
        private readonly ITalentNoteRepository _talentNoteRepository;
        private readonly IAttributesOfTalentRepository _attributesOfTalentRepository;
        private readonly IPlayerAttributeRepository _playerAttributeRepository;
        public TalentServices(ITalentIdentificationRepository talentIdentificationRepository, ITalentNoteRepository talentNoteRepository, IAttributesOfTalentRepository attributesOfTalentRepository, IPlayerAttributeRepository playerAttributeRepository)
        {
            _talentIdentificationRepository = talentIdentificationRepository;
            _talentNoteRepository = talentNoteRepository;
            _attributesOfTalentRepository = attributesOfTalentRepository;
            _playerAttributeRepository = playerAttributeRepository;
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

        #region Talent Notes

        public IEnumerable<TalentNote> GetTalentNotes(int talentId)
        {
            return _talentNoteRepository.GetMany(t => t.TalentIdentificationId == talentId);
        }


        public TalentNote AddTalentNote(TalentNote talentNote)
        {
            return _talentNoteRepository.Add(talentNote);
        }

        public void UpdateTalentNote(TalentNote talentNote)
        {
             _talentNoteRepository.Update(talentNote,talentNote.Id);
        }

        public void DeleteTalentNote(int id)
        {
            _talentNoteRepository.Delete(t => t.Id == id);
        }

        #endregion



        #region Attributes

        public IEnumerable<AttributesOfTalent> GAttributesOfTalents(int talentId,int clubId)
        {
            var presentAttributes = _attributesOfTalentRepository.GetMany(a => a.TalentIdentificationId == talentId);               
            
            var list = new List<AttributesOfTalent>();

            list.AddRange(presentAttributes);

            var leftAttributes =
                _playerAttributeRepository.GetMany(p =>  !presentAttributes.Select(a => a.AttributeId).Contains(p.Id) && p.ClubId == clubId );

            foreach (var attribute in leftAttributes)
            {
                list.Add(new AttributesOfTalent(){AttributeId = attribute.Id,HaveAttribute = false,TalentIdentificationId = talentId,Rating = 0});
            }

            return list;
        }


        public AttributesOfTalent AddAttributesOfTalent(AttributesOfTalent attributesOfTalent)
        {
            return _attributesOfTalentRepository.Add(attributesOfTalent);
        }

        public void UpdateAttributesOfTalent(AttributesOfTalent attributesOfTalent)
        {
            _attributesOfTalentRepository.Update(attributesOfTalent);
        }

        public void DeleteAttributesOfTalent(int talentId, int attributeId)
        {
            _attributesOfTalentRepository.Delete(a => a.AttributeId == attributeId && a.TalentIdentificationId == talentId);
        }

        #endregion

    }
}
