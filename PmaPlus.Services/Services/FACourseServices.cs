using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data;
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

        public IQueryable<FACourse> GetFaCourses()
        {
            return _faCourseRepository.GetAll();
        }

        public bool FaCourseExist(int id)
        {
            return _faCourseRepository.GetMany(f => f.Id == id).Any();
        }

        public FACourse GetFaCourseById(int id)
        {
            return _faCourseRepository.GetById(id);
        }

        public FACourse AddFaCourse(FACourse faCourse)
        {
            return _faCourseRepository.Add(faCourse);
            
        }
        public void UpdateFaCourse(FACourse faCourse, int id)
        {

            faCourse.Id = id;
            _faCourseRepository.Update(faCourse,id);
          
        }

        public void Delete(int id)
        {
            if (id != 0)
                _faCourseRepository.Delete(f => f.Id == id);
        }
    }
}
