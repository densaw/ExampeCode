using System;
using System.ComponentModel.DataAnnotations.Schema;
using PmaPlus.Model;

namespace PmaPlus.Data.Repository
{
    public class PlayerInjuryRepository : RepositoryBase<PlayerInjury>
    {
        public PlayerInjuryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
