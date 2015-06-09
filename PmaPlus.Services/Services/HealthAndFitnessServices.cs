using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;

namespace PmaPlus.Services.Services
{
    public class HealthAndFitnessServices
    {
        private readonly INutritionNewsRepository _nutritionNewsRepository;
        private readonly IExcerciseNewsRepository _excerciseNewsRepository;
        private readonly INutritionRecipeRepository _nutritionRecipeRepository;
        private readonly INutritionFoodTypeRepository _nutritionFoodTypeRepository;

        public HealthAndFitnessServices(INutritionNewsRepository nutritionNewsRepository, IExcerciseNewsRepository excerciseNewsRepository, INutritionRecipeRepository nutritionRecipeRepository, INutritionFoodTypeRepository nutritionFoodTypeRepository)
        {
            _nutritionNewsRepository = nutritionNewsRepository;
            _excerciseNewsRepository = excerciseNewsRepository;
            _nutritionRecipeRepository = nutritionRecipeRepository;
            _nutritionFoodTypeRepository = nutritionFoodTypeRepository;
        }


        public IEnumerable<PictureLinkViewModel> GetNutritionNewsAcordion(int items)
        {
            return _nutritionNewsRepository.GetAll().OrderBy(n => n.NewsDate).Take(items).Select(n => new PictureLinkViewModel() { Id = n.Id, ImgUrl = "api/File/NutritionNews/" + n.MainPicture + "/" + n.Id });
        }

        public string GetLastEcipePicture()
        {
            var recipe = _nutritionRecipeRepository.GetAll().OrderByDescending(r => r.Id).FirstOrDefault();
            if (recipe != null)
                return recipe.Picture;


            return "";
        }

        public IEnumerable<PictureLinkViewModel> GetNutritionRecipesList(int items)
        {
            return _nutritionRecipeRepository.GetAll().OrderByDescending(n => n.Id).Take(items).Select(n => new PictureLinkViewModel() { Id = n.Id, ImgUrl = "api/File/Recipes/" + n.Picture + "/" + n.Id });
        }


        public IEnumerable<PictureLinkViewModel> GetExerciseNewsAcordion(int items)
        {
            return _excerciseNewsRepository.GetAll().OrderBy(n => n.NewsDate).Take(items).Select(n => new PictureLinkViewModel() { Id = n.Id, ImgUrl = "api/File/NutritionNews/" + n.MainPicture + "/" + n.Id });
        }

        public IEnumerable<PictureLinkViewModel> GetNutritionFoodTypesList(int items)
        {

            return _nutritionFoodTypeRepository.GetAll().OrderByDescending(n => n.Id).Take(items).Select(n => new PictureLinkViewModel() { Id = n.Id, ImgUrl = "api/File/FoodTypes/" + n.Picture + "/" + n.Id });
        }
    }
}
