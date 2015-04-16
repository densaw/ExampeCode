using System;
using System.ComponentModel.DataAnnotations.Schema;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Data.Repository
{
    public class PlayerInjuryRepository : RepositoryBase<PlayerInjury>, IPlayerInjuryRepository
    {
        public PlayerInjuryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
