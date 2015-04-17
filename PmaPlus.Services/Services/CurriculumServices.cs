using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services
{
    public class CurriculumServices
    {
        private readonly ICurriculumTypeRepository _curriculumTypeRepository;

        public CurriculumServices(ICurriculumTypeRepository curriculumTypeRepository)
        {
            _curriculumTypeRepository = curriculumTypeRepository;
        }

        public IEnumerable<CurriculumType> GetCurriculumTypes()
        {
            return _curriculumTypeRepository.GetAll();
        }

        public CurriculumType GetCurriculumType(int id)
        {
            return _curriculumTypeRepository.GetById(id);
        }
        public void InsertOrUpdate(CurriculumType curriculumType,int id = 0)
        {
            if (curriculumType.Id == 0)
            {
                _curriculumTypeRepository.Add(curriculumType);

            }
            else
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
