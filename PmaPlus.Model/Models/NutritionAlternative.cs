namespace PmaPlus.Model.Models
{
    public  class NutritionAlternative
    {
        public int Id { get; set; }
        public string BadItemName { get; set; }                 
        public string AlternativeName { get; set; }             
        public string BadItemPicture { get; set; }              
        public string AlternativePicture { get; set; }          
        public string BadItemDescription { get; set; }          
        public string AlternativeDescription { get; set; }      
        public string VideoLink { get; set; }
    }
}
