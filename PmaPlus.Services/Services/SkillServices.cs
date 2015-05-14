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

        public bool SkillLevelExist(int id)
        {
            return _skillLevelRepository.GetMany(s => s.Id == id).Any();
        }

        public IQueryable<SkillLevel> GetSkillLevels()
        {
            return _skillLevelRepository.GetAll();
        }

        public SkillLevel GetSkillLevelById(int id)
        {
            return _skillLevelRepository.GetById(id);
        }

        public SkillLevel AddSkillLevel(SkillLevel skillLevel)
        {
            return _skillLevelRepository.Add(skillLevel);
        }

        public void UpdateSkillLevel(SkillLevel skillLevel, int id)
        {
            if (id != 0)
            {
                skillLevel.Id = id;
                _skillLevelRepository.Update(skillLevel, id);
            }
        }

        public void DeleteSkillLevel(int id)
        {
            _skillLevelRepository.Delete(s => s.Id == id);
        }

        #endregion


        #region SkillVideos

        public IQueryable<SkillVideo> GetSallkillVideos()
        {
            return _skillVideoRepository.GetAll();
        }

        public IQueryable<SkillVideo> GetSkillVideosForSlillLevel(int id)
        {
            return _skillVideoRepository.GetMany(s => s.SkillLevel.Id == id);
        }

        public SkillVideo AddSkillVideo(SkillVideo skillVideo, int id)
        {

            skillVideo.SkillLevel = _skillLevelRepository.GetById(id);
            if (skillVideo.SkillLevel != null)
            {
               return _skillVideoRepository.Add(skillVideo);
            }
            return null;
        }

        public SkillVideo GetSkillVideoById(int id)
        {
            return _skillVideoRepository.GetById(id);
        }

        public bool SkillVideoExist(int id)
        {
            return _skillVideoRepository.GetMany(s => s.Id == id).Any();
        }
        public void UpdateSkillVideo(SkillVideo skillVideo, int id)
        {
            if (id != 0)
            {
                skillVideo.Id = id;
                skillVideo.SkillLevel = _skillVideoRepository.GetById(id).SkillLevel;
                _skillVideoRepository.Update(skillVideo,id);
            }
        }
        #endregion

        public void DeleteSkillVideo(int id)
        {
            _skillVideoRepository.Delete(s => s.Id == id);
        }
    }
}
