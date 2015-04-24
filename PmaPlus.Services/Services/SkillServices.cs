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
        private readonly ISkillVideoRepository _skillVideoRepository;
        private readonly ISkillLevelRepository _skillLevelRepository;

        public SkillServices(ISkillLevelRepository skillLevelRepository, ISkillVideoRepository skillVideoRepository)
        {
            _skillLevelRepository = skillLevelRepository;
            _skillVideoRepository = skillVideoRepository;
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

        public IQueryable<SkillVideo> GetSkillsForSlillLevel(int id)
        {
            return _skillVideoRepository.GetMany(s => s.SkillLevel.Id == id);
        }

        public void AddSkill(SkillVideo skillVideo, int id)
        {
            skillVideo.SkillLevel = _skillLevelRepository.GetById(id);
            if (skillVideo.SkillLevel != null)
            {
                _skillVideoRepository.Add(skillVideo);
            }
        }
        #endregion
    }
}
