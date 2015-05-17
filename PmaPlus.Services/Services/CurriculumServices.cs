using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;

namespace PmaPlus.Services
{
    public class CurriculumServices
    {
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly ICurriculumBlockRepository _curriculumBlockRepository;
        private readonly ICurriculumWeekRepository _curriculumWeekRepository;
        private readonly ICurriculumSessionRepository _curriculumSessionRepository;
        private readonly ICurriculumDetailRepository _curriculumDetailRepository;
        private readonly ICurriculumTypeRepository _curriculumTypeRepository;

        public CurriculumServices(ICurriculumTypeRepository curriculumTypeRepository, ICurriculumRepository curriculumRepository, ICurriculumBlockRepository curriculumBlockRepository, ICurriculumWeekRepository curriculumWeekRepository, ICurriculumSessionRepository curriculumSessionRepository, ICurriculumDetailRepository curriculumDetailRepository)
        {
            _curriculumTypeRepository = curriculumTypeRepository;
            _curriculumRepository = curriculumRepository;
            _curriculumBlockRepository = curriculumBlockRepository;
            _curriculumWeekRepository = curriculumWeekRepository;
            _curriculumSessionRepository = curriculumSessionRepository;
            _curriculumDetailRepository = curriculumDetailRepository;
        }

        #region CurriculumTypes

        public IQueryable<CurriculumTypesTableViewModel> GetCurriculumTypes()
        {
            var types = _curriculumTypeRepository.GetAll();
            var typesView = new List<CurriculumTypesTableViewModel>();
            foreach (var type in types)
            {
                typesView.Add(new CurriculumTypesTableViewModel()
                {
                    Id = type.Id,
                    Name = type.Name,
                    Blocks = type.UsesBlocks,
                    BlockATT = type.UsesBlocksForAttendance,
                    BlockRE = type.UsesBlocksForReports,
                    BlockRTE = type.UsesBlocksForRatings,
                    Weeks = type.UsesWeeks,
                    WeekRE = type.UsesWeeksForReports,
                    WeekATT = type.UsesBlocksForAttendance,
                    WeekRTE = type.UsesWeeksForRatings,
                    Sessions = type.UsesSessions,
                    SessionsRE = type.UsesSessionsForReports,
                    SessionsATT = type.UsesSessionsForAttendance,
                    SessionsRTE = type.UsesSessionsForRatings
                });
            }

            return typesView.AsQueryable();
        }

        public bool CurriculumTypeExist(int id)
        {
            return _curriculumTypeRepository.GetMany(c => c.Id == id).Any();
        }

        public CurriculumType GetCurriculumType(int id)
        {
            return _curriculumTypeRepository.GetById(id);
        }

        public CurriculumType InsertCurriculumType(CurriculumType curriculumType)
        {
            return _curriculumTypeRepository.Add(curriculumType);
        }
        public void UpdateCurriculumTypes(CurriculumType curriculumType, int id)
        {
            curriculumType.Id = id;
            _curriculumTypeRepository.Update(curriculumType, id);
        }
        public void Delete(int id)
        {
            if (id != 0)
                _curriculumTypeRepository.Delete(c => c.Id == id);
        }
        #endregion


        #region Curriculums
        //TODO:Curriculum Progress
        public decimal GetProgress(int curriculumId)
        {
            var curriclum = _curriculumRepository.GetById(curriculumId);
            int sections = curriclum.NumberOfBlocks == 0 ? 1 : curriclum.NumberOfBlocks * curriclum.NumberOfWeeks == 0 ? 1 : curriclum.NumberOfWeeks * curriclum.NumberOfSessions == 0 ? 1 : curriclum.NumberOfSessions;

            return 0;

        }

        public bool CurriculumExist(int id)
        {
            return _curriculumRepository.GetMany(c => c.Id == id).Any();
        }

        public IQueryable<Curriculum> GetAllCurriculums()
        {
            return _curriculumRepository.GetAll();
        }

        public Curriculum AddCurriculum(Curriculum curriculum)
        {
            var curriculumType = _curriculumTypeRepository.Get(t => t.Id == curriculum.CurriculumTypeId);
            if (curriculumType == null)
            {
                return null;
            }

            if (!curriculumType.UsesBlocks)
            {
                curriculum.NumberOfBlocks = 0;
                curriculum.NumberOfWeeks = 0;
                curriculum.NumberOfSessions = 0;
            }
            if (!curriculumType.UsesWeeks)
            {
                curriculum.NumberOfWeeks = 0;
                curriculum.NumberOfSessions = 0;
            }
            if (!curriculumType.UsesSessions)
            {
                curriculum.NumberOfSessions = 0;
            }

            var newCurriculum = _curriculumRepository.Add(curriculum);

            #region MyRegion

            //if (curriculumType.UsesBlocks == true)
            //{
            //    for (int i = 0; i < newCurriculum.NumberOfBlocks; i++)
            //    {
            //        var newBlock = _curriculumBlockRepository.Add(new CurriculumBlock()
            //        {
            //            Curriculum = newCurriculum,
            //        });

            //        if (curriculumType.UsesWeeks == true)
            //        {
            //            for (int j = 0; j < newCurriculum.NumberOfWeeks; j++)
            //            {
            //                var newWeek = _curriculumWeekRepository.Add(new CurriculumWeek()
            //                {
            //                    CurriculumBlock = newBlock
            //                });
            //                if (curriculumType.UsesSessions == true)
            //                {
            //                    for (int k = 0; k < newCurriculum.NumberOfSessions; k++)
            //                    {
            //                        _curriculumSessionRepository.Add(new CurriculumSession()
            //                        {
            //                            CurriculumWeek = newWeek
            //                        });
            //                    }
            //                }
            //                else
            //                {
            //                    newCurriculum.NumberOfSessions = 0;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            newCurriculum.NumberOfWeeks = 0;
            //        }
            //    }
            //}
            //else
            //{
            //    newCurriculum.NumberOfBlocks = 0;
            //}
            #endregion

            //_curriculumRepository.Update(newCurriculum,newCurriculum.Id);
            return newCurriculum;
        }

        public void UpdateCurriculum(Curriculum curriculum, int id)
        {
            curriculum.Id = id;
            _curriculumRepository.Update(curriculum, curriculum.Id);
        }
        #endregion

        #region Details

        public void AddCurriculumDetails(CurriculumDetail curriculumDetail, int curriculumId)
        {
            var curriculum = _curriculumRepository.GetById(curriculumId);
            curriculum.CurriculumDetail = _curriculumDetailRepository.Add(curriculumDetail);
        }

        public CurriculumDetail AddCurriculumBlockDetails(CurriculumDetail curriculumDetail, int curriculumId)
        {
            var curriculum = _curriculumRepository.GetById(curriculumId);
            if (curriculum.NumberOfBlocks > curriculum.CurriculumBlocks.Count)
            {
                var curriculumBlock = _curriculumBlockRepository.Add(new CurriculumBlock()
                {
                    Curriculum = curriculum
                });
                return curriculumBlock.CurriculumDetail = _curriculumDetailRepository.Add(curriculumDetail);
            }
            return null;
        }
        public CurriculumDetail AddCurriculumWeekDetails(CurriculumDetail curriculumDetail, int curriculumBlockId)
        {
            var curriculumBlock = _curriculumBlockRepository.GetById(curriculumBlockId);

            if (curriculumBlock.Curriculum.NumberOfWeeks > curriculumBlock.CurriculumWeeks.Count)
            {
                var curriculumWeek = _curriculumWeekRepository.Add(new CurriculumWeek()
                {
                    CurriculumBlock = curriculumBlock
                });
                return curriculumWeek.CurriculumDetail = _curriculumDetailRepository.Add(curriculumDetail);
            }
            return null;
        }
        public CurriculumDetail AddCurriculumSessionDetails(CurriculumDetail curriculumDetail, int curriculumWeekId)
        {
            var curriculumWeek = _curriculumWeekRepository.GetById(curriculumWeekId);
            if (curriculumWeek.CurriculumBlock.Curriculum.NumberOfSessions > curriculumWeek.CurriculumSessions.Count)
            {
                var curriculum = _curriculumSessionRepository.Add(new CurriculumSession()
                {
                    CurriculumWeek = curriculumWeek
                });
                return curriculum.CurriculumDetail = _curriculumDetailRepository.Add(curriculumDetail);
            }
            return null;
        }
        #endregion
    }
}
