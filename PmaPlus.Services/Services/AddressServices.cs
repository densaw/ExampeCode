using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class AddressServices
    {
        private readonly IAddressRepository _addressRepository;

        public AddressServices(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public Address GetAddressById(int id)
        {
            return _addressRepository.GetById(id);
        }

        public Address AddAddress(Address address)
        {
            return _addressRepository.Add(address);
        }

        public void UpdateAddress(Address address)
        {
            _addressRepository.Update(address);
        }

        public void DeleteAddress(int id)
        {
            _addressRepository.Delete(a => a.Id == id);
        }




    }
}
