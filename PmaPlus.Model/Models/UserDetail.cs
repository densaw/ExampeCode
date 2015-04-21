using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmaPlus.Model.Models
{
    public class UserDetail
    {
      
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        
        public DateTime? Birthday { get; set; }

        public string AboutMe { get; set; }

        public int? FaNumber { get; set; }

        public string ProfilePicture { get; set; }

        public string Nationality { get; set; }

        public virtual Address Address { get; set; }

    }
}
