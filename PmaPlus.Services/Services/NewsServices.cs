using System.Linq;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class NewsServices
    {
        private readonly IExcerciseNewsRepository _excerciseNewRepository;
        private readonly INutritionNewsRepository _nutritionNewsRepository;

        public NewsServices(IExcerciseNewsRepository excerciseNewRepository, INutritionNewsRepository nutritionNewsRepository)
        {
            _excerciseNewRepository = excerciseNewRepository;
            _nutritionNewsRepository = nutritionNewsRepository;
        }


        #region NutritionNews

        public bool NutritionNewExist(int id)
        {
            return _nutritionNewsRepository.GetMany(n => n.Id == id).Any();
        }


        public IQueryable<NutritionNew> GetNutritionNews()
        {
            return _nutritionNewsRepository.GetAll();
        }

        public NutritionNew GetNutritionNewById(int id)
        {
            return _nutritionNewsRepository.GetById(id);
        }

        public NutritionNew AddNutrittionNew(NutritionNew nutritionNew)
        {
            return _nutritionNewsRepository.Add(nutritionNew);
        }


        public void UpdateNutritionNew(NutritionNew nutritionNew,int id)
        {
            nutritionNew.Id = id;
            _nutritionNewsRepository.Update(nutritionNew,nutritionNew.Id);
        }

        public void DeleteNutritionNew(int id)
        {
            _nutritionNewsRepository.Delete(n => n.Id == id);
        }

        #endregion

        #region ExerciseNews


        public bool ExerciseNewExist(int id)
        {
            return _excerciseNewRepository.GetMany(n => n.Id == id).Any();
        }
        public IQueryable<ExcerciseNew> GetExcerciseNews()
        {
            return _excerciseNewRepository.GetAll();
        }

        public ExcerciseNew GetExcerciseNewById(int id)
        {
            return _excerciseNewRepository.GetById(id);
        }

        public ExcerciseNew AddExcerciseNew(ExcerciseNew excerciseNew)
        {
            return _excerciseNewRepository.Add(excerciseNew);
        }


        public void UpdateExerciseNew(ExcerciseNew excerciseNew, int id)
        {
            excerciseNew.Id = id;
            _excerciseNewRepository.Update(excerciseNew,excerciseNew.Id);
        }

        public void DeleteExcerciseNew(int id)
        {
            _excerciseNewRepository.Delete(e => e.Id == id);
        }

        #endregion



    }
}
