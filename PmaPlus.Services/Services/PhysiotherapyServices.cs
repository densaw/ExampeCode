using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;

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





    }
}
