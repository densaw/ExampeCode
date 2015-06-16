using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.ViewModels.Matches
{
    public class PlayerMatchStatisticTableViewModel
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPicture { get; set; }
        public int MatchId { get; set; }
        public int Goals { get; set; }
        public int Shots { get; set; }
        public int ShotsOnTarget { get; set; }
        public int Assists { get; set; }
        public int Tackles { get; set; }
        public int Passes { get; set; }
        public int Saves { get; set; }
        public int Corners { get; set; }
        public int FreeKicks { get; set; }
        [Required]
        public int FormRating { get; set; }
        public int PlayingTime { get; set; }
    }
}