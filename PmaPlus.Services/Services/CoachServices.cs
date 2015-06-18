using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;

namespace PmaPlus.Services.Services
{
    public class CoachServices
    {
        private readonly ICoachRepository _coachRepository;
        private readonly IUserRepository _userRepository;

        public CoachServices(IUserRepository userRepository, ICoachRepository coachRepository)
        {
            _userRepository = userRepository;
            _coachRepository = coachRepository;
        }

        public bool CoachExist(int id)
        {
            return _coachRepository.GetMany(c => c.Id == id).Any();
        }



    }
}
