using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class FaCourseServices
    {
        private readonly IFACourseRepository _faCourseRepository;

        public FaCourseServices(IFACourseRepository faCourseRepository)
        {
            _faCourseRepository = faCourseRepository;
        }

        public IEnumerable<FACourse> GetFaCourses()
        {
            return _faCourseRepository.GetAll();
        }

        public FACourse GetFaCourse(int id)
        {
            return _faCourseRepository.GetById(id);
        }

        public void InsertOrUpdate(FACourse faCourse)
        {
            if (faCourse.Id == 0)
            {
            _faCourseRepository.Add(faCourse);
                
            }
            else
            {
                _faCourseRepository.Update(faCourse);
            }
        }

        public void Delete(int id)
        {
            if (id != 0)
                _faCourseRepository.Delete(f => f.Id == id);
        }
    }
}
