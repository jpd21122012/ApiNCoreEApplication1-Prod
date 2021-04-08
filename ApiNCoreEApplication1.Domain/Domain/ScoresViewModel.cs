namespace ApiNCoreEApplication1.Domain
{
    public class ScoresViewModel : BaseDomain
    {
        public int IdSeason { get; set; }
        public int IdTournament { get; set; }
        public int IdMatch { get; set; }
        public int IdTeam { get; set; }
        public string PlayedMatches { get; set; }
        public string WonMatches { get; set; }
        public string TiedMatches { get; set; }
        public string LostMatches { get; set; }
        public string ScoredGoals { get; set; }
        public string ReceivedGoals { get; set; }
        public string TotalTeamPoints { get; set; }
    }
}
