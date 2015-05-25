using System;
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
        private readonly IScenarioRepository _scenarioRepository;

        public CurriculumServices(ICurriculumTypeRepository curriculumTypeRepository, ICurriculumRepository curriculumRepository, ICurriculumBlockRepository curriculumBlockRepository, ICurriculumWeekRepository curriculumWeekRepository, ICurriculumSessionRepository curriculumSessionRepository, ICurriculumDetailRepository curriculumDetailRepository, IClubRepository clubRepository, ICurriculumStatementRepository curriculumStatementRepository, IStatementRolesRepository statementRolesRepository, IScenarioRepository scenarioRepository)
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
            _scenarioRepository = scenarioRepository;
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
                    CurriculumDetail = new CurriculumDetail()
                });


                for (int j = 0; j < newCurriculum.NumberOfWeeks; j++)
                {
                    var newWeek = _curriculumWeekRepository.Add(new CurriculumWeek()
                    {
                        CurriculumBlock = newBlock,
                        CurriculumDetail = new CurriculumDetail()
                    });

                    for (int k = 0; k < newCurriculum.NumberOfSessions; k++)
                    {
                        _curriculumSessionRepository.Add(new CurriculumSession()
                        {
                            CurriculumWeek = newWeek,
                            CurriculumDetail = new CurriculumDetail()
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

        public void DeleteCurriculum(int id)
        {
            _curriculumRepository.Delete(c => c.Id == id);
        }
        #endregion

        #region Details

        public Curriculum GetCurriculumDetails(int curriculumId)
        {
            return _curriculumRepository.GetById(curriculumId);
        }

        public IEnumerable<CurriculumBlock> GetCurriculumBlocks(int curriculumId)
        {
            return _curriculumRepository.GetById(curriculumId).CurriculumBlocks;
        }

        public IEnumerable<CurriculumWeek> GetCurriculumWeeks(int curriculumBlockId)
        {
            return _curriculumBlockRepository.GetById(curriculumBlockId).CurriculumWeeks;
        }

        public IEnumerable<CurriculumSession> GetCurriculumSessionDetails(int curriculumWeekId)
        {
            return _curriculumWeekRepository.GetById(curriculumWeekId).CurriculumSessions;
        }

        public CurriculumDetail AddCurriculumDetails(CurriculumDetail curriculumDetail, int curriculumId,int scenariooId)
        {
            var curriculum = _curriculumRepository.GetById(curriculumId);
            if (curriculum.CurriculumDetail != null)
            {
                curriculum.CurriculumDetail.Name = curriculumDetail.Name;
                curriculum.CurriculumDetail.Number = curriculumDetail.Number;
                curriculum.CurriculumDetail.PlayersDescription = curriculumDetail.PlayersDescription;
                curriculum.CurriculumDetail.PlayersFriendlyName = curriculumDetail.PlayersFriendlyName;
                curriculum.CurriculumDetail.CoachDescription = curriculumDetail.CoachDescription;
                curriculum.CurriculumDetail.CoachPicture = curriculumDetail.CoachPicture;
                curriculum.CurriculumDetail.PlayersFriendlyPicture = curriculumDetail.PlayersFriendlyPicture;

                curriculum.CurriculumDetail.Scenario = _scenarioRepository.GetById(scenariooId);

                return null;
            }
            else
            {
                
            var newDetail = _curriculumDetailRepository.Add(curriculumDetail);
            curriculum.CurriculumDetail = newDetail;
            _curriculumRepository.Update(curriculum, curriculum.Id);
            return newDetail;
            }
        }
        public CurriculumDetail AddCurriculumBlockDetails(CurriculumDetail curriculumDetail, int curriculumBlockId)
        {
            var curriculumBlock = _curriculumBlockRepository.GetById(curriculumBlockId);

            var newDetail = curriculumBlock.CurriculumDetail = _curriculumDetailRepository.Add(curriculumDetail);

            _curriculumBlockRepository.Update(curriculumBlock, curriculumBlock.Id);

            return newDetail;
        }
        public CurriculumDetail AddCurriculumWeekDetails(CurriculumDetail curriculumDetail, int curriculumWeekId)
        {
            var curriculumWeek = _curriculumWeekRepository.GetById(curriculumWeekId);

            var newDetail = curriculumWeek.CurriculumDetail = _curriculumDetailRepository.Add(curriculumDetail);

            _curriculumWeekRepository.Update(curriculumWeek, curriculumWeek.Id);

            return newDetail;
        }
        public CurriculumDetail AddCurriculumSessionDetails(CurriculumDetail curriculumDetail, int curriculumSessionId)
        {
            var curriculumSession = _curriculumSessionRepository.GetById(curriculumSessionId);

            var newDetail = curriculumSession.CurriculumDetail = _curriculumDetailRepository.Add(curriculumDetail);

            _curriculumSessionRepository.Update(curriculumSession, curriculumSession.Id);

            return newDetail;
        }

        public void UpdateDetail(CurriculumDetail curriculumDetail, int id)
        {
            curriculumDetail.Id = id;
            _curriculumDetailRepository.Update(curriculumDetail, curriculumDetail.Id);
        }

        #endregion

        #region Statements

        public bool StatementExist(int id)
        {
            return _curriculumStatementRepository.GetMany(s => s.Id == id).Any();
        }

        public IQueryable<CurriculumStatement> GetCurriculumStatements(int clubId)
        {
            return _curriculumStatementRepository.GetMany(s => s.Club.Id == clubId);
        }


        public CurriculumStatement GetCurriculumStatementById(int id)
        {
            return _curriculumStatementRepository.GetById(id);
        }

        public void AddCurricululmStatment(CurriculumStatement statement, IList<Role> rolesList, int clubId)
        {
            statement.Club = _clubRepository.GetById(clubId);
            var newStatment = _curriculumStatementRepository.Add(statement);

            if (newStatment != null)
            {
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
