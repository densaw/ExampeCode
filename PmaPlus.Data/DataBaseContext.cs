using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model;

namespace PmaPlus.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<Address> Addresses { get; set; }




    }
}
