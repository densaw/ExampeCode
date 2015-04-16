using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
