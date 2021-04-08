using System;
namespace ApiNCoreEApplication1.Domain
{
    public class TournamentsViewModel : BaseDomain
    {
        public int IdSeason { get; set; }
        public int IdTeam { get; set; }
        public int IdRivalTeam { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
