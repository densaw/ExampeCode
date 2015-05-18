using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels
{
    public class AddPlayerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }                               
        public string  LastName { get; set; }
        public List<int> Teams { get; set; } 
        public UserStatus UserStatus { get; set; }                          
        public string Email { get; set; }                                   
        public string Password { get; set; }                                
        public string Telephone { get; set; }                               
        public string  Mobile { get; set; }                                 
        public int? FaNumber { get; set; }                                   
        public DateTime? BirthDate { get; set; }
        public Foot PlayingFoot { get; set; }
        public string ProfilePicture { get; set; }                          
        public string Nationality { get; set; }                             
        public string Address1 { get; set; }                                
        public string Address2 { get; set; }                                
        public string Address3 { get; set; }                                
        public string TownCity { get; set; }                                
        public string Postcode { get; set; }

        public string ParentsFirstName { get; set; }
        public string ParentsLastName { get; set; }
        public string ParentsContactNumber { get; set; }
        public string PlayerHealthConditions { get; set; }
        public string SchoolName { get; set; }
        public string SchoolTelephone { get; set; }
        public string SchoolContactName { get; set; }
        public string SchoolContactEmail { get; set; }
        public string SchoolAddress1 {get;set;}
        public string SchoolAddress2 {get;set;}
        public string SchoolTownCity {get;set;}
        public string SchoolPostcode { get; set; }

    }
}
