using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class PlayerAttributeServices
    {
        private readonly IPlayerAttributeRepository _playerAttributeRepository;

        public PlayerAttributeServices(IPlayerAttributeRepository playerAttributeRepository)
        {
            _playerAttributeRepository = playerAttributeRepository;
        }

        public bool PlayerAttributeExist(int id)
        {
            return _playerAttributeRepository.GetMany(s => s.Id == id).Any();
        }

        public IQueryable<PlayerAttribute> GetPlayerAttributes(int clubId)
        {
            return _playerAttributeRepository.GetMany(a =>a.ClubId == clubId);
        }

        public PlayerAttribute GetPlayerAttributeById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return _playerAttributeRepository.GetById(id);
        }

        public PlayerAttribute AddPlayerAttribute(PlayerAttribute playerAttribute)
        {
            if (playerAttribute == null)
            {
                return null;
            }

           
            return _playerAttributeRepository.Add(playerAttribute);
        }

        public void UpdatePlayerAttribute(PlayerAttribute playerAttribute, int id)
        {
            if (id != 0)
            {
                playerAttribute.Id = id;
                _playerAttributeRepository.Update(playerAttribute, id);
            }
        }

        public void DeletePlayerAttribute(int id)
        {
            if (id != 0)
            {
                _playerAttributeRepository.Delete(s => s.Id == id);
            }
        }


    }
}
