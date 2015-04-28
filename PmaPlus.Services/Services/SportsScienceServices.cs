using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class SportsScienceServices
    {
        private readonly ISportsScienceTestRepository _sportsScienceTestRepository;
        private readonly ISportsScienceExerciseRepository _sportsScienceExerciseRepository;

        public SportsScienceServices(ISportsScienceExerciseRepository sportsScienceExerciseRepository, ISportsScienceTestRepository sportsScienceTestRepository)
        {
            _sportsScienceExerciseRepository = sportsScienceExerciseRepository;
            _sportsScienceTestRepository = sportsScienceTestRepository;
        }

        #region Tests

        public bool SportsScienceTestExist(int id)
        {
            return _sportsScienceTestRepository.GetMany(t => t.Id == id).Any();
        }

        public IQueryable<SportsScienceTest> GetSportsScienceTests()
        {
            return _sportsScienceTestRepository.GetAll();
        }

        public SportsScienceTest GetSportsScienceTestById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return _sportsScienceTestRepository.GetById(id);
        }

        public SportsScienceTest AddSportsScienceTest(SportsScienceTest test)
        {
            if (test != null)
            {
                return _sportsScienceTestRepository.Add(test);
            }
            return null;
        }

        public void UpdateSportsScienceTest(SportsScienceTest test, int id)
        {
            if (id != 0)
            {
                test.Id = id;
                _sportsScienceTestRepository.Update(test,id);
            }
        }

        public void DeleteSportsScienceTest(int id)
        {
            _sportsScienceTestRepository.Delete(t => t.Id == id);
        }

        #endregion


        #region Exercises
        public bool SportsScienceExerciseExist(int id)
        {
            return _sportsScienceExerciseRepository.GetMany(t => t.Id == id).Any();
        }

        public IQueryable<SportsScienceExercise> GetSportsScienceExercises()
        {
            return _sportsScienceExerciseRepository.GetAll();
        }

        public SportsScienceExercise GetSportsScienceExerciseById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return _sportsScienceExerciseRepository.GetById(id);
        }

        public SportsScienceExercise AddSportsScienceExercise(SportsScienceExercise exercise)
        {
            if (exercise != null)
            {
                return _sportsScienceExerciseRepository.Add(exercise);
            }
            return null;
        }

        public void UpdateSportsScienceExercise(SportsScienceExercise exercise, int id)
        {
            if (id != 0)
            {
                exercise.Id = id;
                _sportsScienceExerciseRepository.Update(exercise, id);
            }
        }

        public void DeleteSportsScienceExercise(int id)
        {
            _sportsScienceExerciseRepository.Delete(t => t.Id == id);
        }

        #endregion
    }
}
