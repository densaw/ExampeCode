using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;

namespace PmaPlus.Model
{
    public class User : IUser<int>
    {
        public User()
        {
            Comments = new List<MessageComment>();
            Ratings = new List<MessageRating>();
            MessagePrivates = new List<MessagePrivate>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime LoggedAt { get; set; }
        public virtual UserDetail UserDetail { get; set; }

        public virtual ICollection<Qualification> Qualifications { get; set; }
        public virtual ICollection<MessageComment> Comments { get; set; }
        public virtual ICollection<MessageRating> Ratings { get; set; }
        public virtual ICollection<MessagePrivate> MessagePrivates { get; set; } 
        public virtual ICollection<MessageGroup> MessageGroups { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
