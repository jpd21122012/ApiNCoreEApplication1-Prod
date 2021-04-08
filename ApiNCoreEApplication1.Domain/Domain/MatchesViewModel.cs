namespace ApiNCoreEApplication1.Domain
{
    public class MatchesViewModel : BaseDomain
    {
        public int RivalIdTeam { get; set; }
        public int IdTeam { get; set; }
        public string TotalScoreTeam { get; set; }
        public string TotalScoreRivalTeam { get; set; }
    }
}
