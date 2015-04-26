using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class PhysiotherapyServices
    {
        private readonly IPhysiotherapyExerciseRepository _physiotherapyExerciseRepository;
        private readonly IBodyPartRepository _bodyPartRepository;

        public PhysiotherapyServices(IBodyPartRepository bodyPartRepository, IPhysiotherapyExerciseRepository physiotherapyExerciseRepository)
        {
            _bodyPartRepository = bodyPartRepository;
            _physiotherapyExerciseRepository = physiotherapyExerciseRepository;
        }

        #region BodyParts

        public bool BodyPartExist(int id)
        {
            return _bodyPartRepository.GetMany(b => b.Id == id).Any();
        }

        public IQueryable<BodyPart> GetBodyParts()
        {
            return _bodyPartRepository.GetAll();
        }

        public BodyPart GetBodyPartById(int id)
        {
            return _bodyPartRepository.GetById(id);
        }

        public BodyPart AddBodyPart(BodyPart bodyPart)
        {
           return _bodyPartRepository.Add(bodyPart);
        }

        public void UpdateBodyPart(BodyPart bodyPart, int id)
        {
            if (id != 0)
            {
                bodyPart.Id = id;
                _bodyPartRepository.Update(bodyPart, id);
            }
        }

        public void DeleteBodyPart(int id)
        {
            _bodyPartRepository.Delete(b => b.Id == id);
        }
        #endregion


        #region Exercise

        public bool ExerciseExist(int id)
        {
            return _physiotherapyExerciseRepository.GetMany(e => e.Id == id).Any();
        }

        public IQueryable<PhysiotherapyExercise> GetExercises()
        {
            return _physiotherapyExerciseRepository.GetAll();
        }

        public PhysiotherapyExercise GetExerciseById(int id)
        {
            return _physiotherapyExerciseRepository.GetById(id);
        }

        public PhysiotherapyExercise AddExercise(PhysiotherapyExercise exercise)
        {
          return  _physiotherapyExerciseRepository.Add(exercise);
        }

        public void UpdateExercise(PhysiotherapyExercise exercise, int id)
        {
            if (id != 0)
            {
                exercise.Id = id;
                _physiotherapyExerciseRepository.Update(exercise, id);
            }
        }

        public void DeleteExercise(int id)
        {
            _physiotherapyExerciseRepository.Delete(e => e.Id == id);
        }

        #endregion

    }
}
