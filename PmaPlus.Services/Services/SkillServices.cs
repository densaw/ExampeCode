using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class SkillServices
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillLevelRepository _skillLevelRepository;

        public SkillServices(ISkillLevelRepository skillLevelRepository, ISkillRepository skillRepository)
        {
            _skillLevelRepository = skillLevelRepository;
            _skillRepository = skillRepository;
        }

        #region Skill Levels


        public IEnumerable<SkillLevel> GetSkillLevels()
        {
            return _skillLevelRepository.GetAll().AsEnumerable();
        }

        public SkillLevel GetSkillLevelById(int id)
        {
            return _skillLevelRepository.GetById(id);
        }

        public void AddSkillLevel(SkillLevel skillLevel)
        {
            _skillLevelRepository.Add(skillLevel);
        }

        public void UpdateSkillLevel(SkillLevel skillLevel, int id)
        {
            if (skillLevel.Id != 0)
            {
                _skillLevelRepository.Update(skillLevel, id);
            }
        }

        public void DeleteSkillLevel(int id)
        {
            _skillLevelRepository.Delete(s => s.Id == id);
        }

        #endregion


        #region Skills

        public IEnumerable<Skill> GetSkillsForSlillLevel(int id)
        {
            return _skillRepository.GetMany(s => s.SkillLevel.Id == id).AsEnumerable();
        }

        public void AddSkill(Skill skill, int id)
        {
            skill.SkillLevel = _skillLevelRepository.GetById(id);
            if (skill.SkillLevel != null)
            {
                _skillRepository.Add(skill);
            }
        }
        #endregion
    }
}
