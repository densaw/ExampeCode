using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int Number { get; set; }                                           //Session Number:	Numeric (e.g. 1)	
        public string Name { get; set; }                                            //Session Name:		Text Field
        public bool Attendance { get; set; }                                            //Attendance:	Yes\No (The coach will simply indicate if each player attended or not)
        public bool Objectives { get; set; }                                            //Objectives:	Yes\No	(the coach will have the option to add objectives to each player)
        public bool Rating { get; set; }                                            //Ratings:	Yes\No	(the coach will have to indicate how well the player did)
        public bool Report { get; set; }                                            //Report:	Yes\No	(the coach will have to add a report on how well the player has done so far.
        public bool ObjectiveReport { get; set; }                                            //Objective Report:	Yes\No (the coach will have to report on if the last objectives were achieved or not)
        public bool CoachDetails { get; set; }                                      //Coach Details:	Yes\No	
        public string CoachPicture { get; set; }                                            //Coach Picture:	If yes above then user adds picture (upload)
        public string CoachDetailsName { get; set; }                                            //Coach Details Name	If yes above then user adds name (text field)
        public bool PlayerDetails { get; set; }                                            //Player Details:	Yes\No
        public string  PlayerPicture { get; set; }                                            //Player Picture:	If yes above then user adds picture (upload)
        public string PlayerDetailsName { get; set; }                                            //Player Details Name:	If yes above then user adds name (text field)
        public bool NeedScenarios { get; set; }                                            //Scenarios:	Yes\No
        public virtual ICollection<Scenario> Scenarios { get; set; }                                            //Sceanario Pictures:	If yes above user can add 1 or more scenarios
        public virtual Curriculum Curriculum { get; set; }
    }
}
