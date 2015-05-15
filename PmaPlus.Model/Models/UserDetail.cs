using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmaPlus.Model.Models
{
    public class UserDetail
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public DateTime? Birthday { get; set; }

        public string AboutMe { get; set; }

        public int? FaNumber { get; set; }

        public string ProfilePicture { get; set; }

        public string Nationality { get; set; }

        public virtual Address Address { get; set; }

        public DateTime? CrbDbsExpiry { get; set; }
        public DateTime? FirstAidExpiry { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }

    }
}
