using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.TrainingTeamMember
{
    public class AddTrainingTeamMemberViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }                               
        public string  LastName { get; set; }                               
        public Role Role { get; set; }                                      
        public UserStatus UserStatus { get; set; }                          
        public bool NeedReport { get; set; }                                
        public string AboutMe { get; set; }                                 
        public string Telephone { get; set; }                               
        public string  Mobile { get; set; }                                 
        public string Email { get; set; }                                   
        public string Password { get; set; }                                
        public int FaNumber { get; set; }                                   
        public DateTime? BirthDate { get; set; }                             
        public string ProfilePicture { get; set; }                          
        public string Nationality { get; set; }                             
        public string Address1 { get; set; }                                
        public string Address2 { get; set; }                                
        public string Address3 { get; set; }                                
        public string TownCity { get; set; }                                
        public string Postcode { get; set; }
        public DateTime? CrbDbsExpiry { get; set; }
        public DateTime? FirstAidExpiry { get; set; }
    }
}
