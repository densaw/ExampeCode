﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model;

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
        private readonly IClubRepository _clubRepository;
        private readonly IStatementRolesRepository _statementRolesRepository;
        private readonly ICurriculumStatementRepository _curriculumStatementRepository;

        public CurriculumServices(ICurriculumTypeRepository curriculumTypeRepository, ICurriculumRepository curriculumRepository, ICurriculumBlockRepository curriculumBlockRepository, ICurriculumWeekRepository curriculumWeekRepository, ICurriculumSessionRepository curriculumSessionRepository, ICurriculumDetailRepository curriculumDetailRepository, IClubRepository clubRepository, ICurriculumStatementRepository curriculumStatementRepository, IStatementRolesRepository statementRolesRepository)
        {
            _curriculumTypeRepository = curriculumTypeRepository;
            _curriculumRepository = curriculumRepository;
            _curriculumBlockRepository = curriculumBlockRepository;
            _curriculumWeekRepository = curriculumWeekRepository;
            _curriculumSessionRepository = curriculumSessionRepository;
            _curriculumDetailRepository = curriculumDetailRepository;
            _clubRepository = clubRepository;
            _curriculumStatementRepository = curriculumStatementRepository;
            _statementRolesRepository = statementRolesRepository;
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

        public IEnumerable<CurriculumType> GetAllTypes()
        {
            return _curriculumTypeRepository.GetAll();
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

        public IEnumerable<CurriculumsList> GetClubCurriculumsList(int clubId)
        {
            return from curri in _curriculumRepository.GetMany(c => c.Club.Id == clubId)
                   select new CurriculumsList()
                   {
                       Id = curri.Id,
                       Name = curri.Name
                   };
        }

        public IQueryable<Curriculum> GetClubCurriculums(int clubId)
        {
            return _curriculumRepository.GetMany(c => c.Club.Id == clubId);
        }

        public Curriculum AddCurriculum(Curriculum curriculum, int clubId)
        {
            var club = _clubRepository.GetById(clubId);

            if (club == null)
            {
                return null;
            }

            curriculum.Club = club;

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

          


            for (int i = 0; i < newCurriculum.NumberOfBlocks; i++)
            {
                var newBlock = _curriculumBlockRepository.Add(new CurriculumBlock()
                {
                    Curriculum = newCurriculum,
                });


                for (int j = 0; j < newCurriculum.NumberOfWeeks; j++)
                {
                    var newWeek = _curriculumWeekRepository.Add(new CurriculumWeek()
                    {
                        CurriculumBlock = newBlock
                    });

                    for (int k = 0; k < newCurriculum.NumberOfSessions; k++)
                    {
                        _curriculumSessionRepository.Add(new CurriculumSession()
                        {
                            CurriculumWeek = newWeek
                        });
                    }

                }

            }

            return newCurriculum;
        }

        public void UpdateCurriculum(Curriculum curriculum, int id)
        {
            curriculum.Id = id;
            _curriculumRepository.Update(curriculum, curriculum.Id);
        }
        #endregion

        #region Details

        public IEnumerable<CurriculumDetail> GetCurriculumDetails(int curriculumId)
        {
            return _curriculumDetailRepository.GetMany(d => d.Id == _curriculumRepository.GetById(curriculumId).Id);
        }

        public IEnumerable<CurriculumDetail> GetCurriculumBlockDetails(int curriculumBlockId)
        {
            return _curriculumDetailRepository.GetMany(d => d.Id == _curriculumBlockRepository.GetById(curriculumBlockId).Id);
        }

        public IEnumerable<CurriculumDetail> GetCurriculumWeekDetails(int curriculumWeekId)
        {
            return _curriculumDetailRepository.GetMany(d => d.Id == _curriculumWeekRepository.GetById(curriculumWeekId).Id);
        }

        public IEnumerable<CurriculumDetail> GetCurriculumSessionDetails(int curriculumSessionId)
        {
            return _curriculumDetailRepository.GetMany(d => d.Id == _curriculumSessionRepository.GetById(curriculumSessionId).Id);
        }


        public CurriculumDetail AddCurriculumDetails(CurriculumDetail curriculumDetail, int curriculumId)
        {
            var curriculum = _curriculumRepository.GetById(curriculumId);
            if (curriculum.CurriculumDetail != null)
            {
                return null;
            }
            return curriculum.CurriculumDetail = _curriculumDetailRepository.Add(curriculumDetail);
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

        #region Statements

        public bool StatementExist(int id)
        {
            return _curriculumStatementRepository.GetMany(s => s.Id == id).Any();
        }

        public IEnumerable<CurriculumStatement> GetCurriculumStatements(int clubId)
        {
            return _curriculumStatementRepository.GetMany(s => s.Club.Id == clubId);
        }


        public void AddCurricululmStatment(CurriculumStatement statement, IList<Role> rolesList, int clubId)
        {
            statement.Club = _clubRepository.GetById(clubId);
            var newStatment = _curriculumStatementRepository.Add(statement);
            foreach (var role in rolesList)
            {
                _statementRolesRepository.Add(new StatementRoles()
                {
                    Role = role,
                    Statement = newStatment,
                    CurriculumStatementId = newStatment.Id
                });
            }
        }


        public void UpdateCurriculumStatment(CurriculumStatement statement, IList<Role> rolesList, int id)
        {
            if (id != 0)
            {
                statement.Id = id;
                _curriculumStatementRepository.Update(statement, id);

                var tempStatement = _curriculumStatementRepository.GetById(id);
                foreach (var role in rolesList)
                {
                    if (!_statementRolesRepository.GetMany(s => s.CurriculumStatementId == tempStatement.Id && s.Role == role).Any())
                    {
                        _statementRolesRepository.Add(new StatementRoles()
                        {
                            Role = role,
                            Statement = tempStatement,
                            CurriculumStatementId = tempStatement.Id
                        });
                    }
                }
                _statementRolesRepository.Delete(s => s.CurriculumStatementId == tempStatement.Id && !rolesList.Contains(s.Role));
            }
        }

        public void DeleteCurriculumStatement(int id)
        {
            if (id != 0)
            {
                _statementRolesRepository.Delete(sr => sr.CurriculumStatementId == id);
                _curriculumStatementRepository.Delete(s => s.Id == id);
            }
        }

        #endregion
    }
}
