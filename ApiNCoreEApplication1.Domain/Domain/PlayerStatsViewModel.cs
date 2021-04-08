namespace ApiNCoreEApplication1.Domain
{
    public class PlayerStatsViewModel : BaseDomain
    {
        public int IdPlayer { get; set; }
        public int IdSeason { get; set; }
        public int IdTournament { get; set; }
        public int IdMatch { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
