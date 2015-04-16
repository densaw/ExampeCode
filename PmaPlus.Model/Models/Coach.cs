namespace PmaPlus.Model.Models
{
    public class Coach
    {
        
        public int Id { get; set; }

        

        public int? TeamId { get; set; }

        public virtual Club Club { get; set; }

        public virtual Team Team { get; set; }

        public virtual User User { get; set; }
    }
}
