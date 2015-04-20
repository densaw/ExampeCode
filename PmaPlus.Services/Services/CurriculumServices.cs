﻿using System;
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
        private readonly ICurriculumTypeRepository _curriculumTypeRepository;

        public CurriculumServices(ICurriculumTypeRepository curriculumTypeRepository)
        {
            _curriculumTypeRepository = curriculumTypeRepository;
        }

        public IEnumerable<CurriculumTypesTableViewModel> GetCurriculumTypes()
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
                    BlockRTE = type.UsesBlocksForRatings,
                    Weeks = type.UsesWeeks,
                    WeekATT = type.UsesBlocksForAttendance,
                    WeekRTE = type.UsesWeeksForRatings,
                    Sessions = type.UsesSessions,
                    SessionsATT = type.UsesSessionsForAttendance,
                    SessionsRTE = type.UsesSessionsForRatings
                });
            }

            return typesView;
        }

        public CurriculumType GetCurriculumType(int id)
        {
            return _curriculumTypeRepository.GetById(id);
        }

        public void InsertCurriculumType(CurriculumType curriculumType)
        {
            _curriculumTypeRepository.Add(curriculumType);
        }
        public void UpdateCurriculumTypes(CurriculumType curriculumType, int id)
        {
            if (curriculumType.Id != 0)
            {
                _curriculumTypeRepository.Update(curriculumType);
            }
        }
        public void Delete(int id)
        {
            if (id != 0)
                _curriculumTypeRepository.Delete(c => c.Id == id);
        }

    }
}