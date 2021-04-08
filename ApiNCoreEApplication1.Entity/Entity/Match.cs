namespace ApiNCoreEApplication1.Entity
{
    public class Match : BaseEntity
    {
        public int RivalIdTeam { get; set; }
        public int IdTeam { get; set; }
        public string TotalScoreTeam { get; set; }
        public string TotalScoreRivalTeam { get; set; }
    }
}
