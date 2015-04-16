namespace PmaPlus.Model.Models
{
    public  class HeadOfEducation
    {
       
        public int Id { get; set; }

     

        public virtual Club Club { get; set; }

        public virtual User User { get; set; }
    }
}
