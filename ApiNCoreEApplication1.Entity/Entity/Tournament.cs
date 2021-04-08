using System;
namespace ApiNCoreEApplication1.Entity
{
    public class Tournament : BaseEntity
    {
        public int IdSeason { get; set; }
        public int IdTeam { get; set; }
        public int IdRivalTeam { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
