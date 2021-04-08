namespace ApiNCoreEApplication1.Domain
{
    public class AwardsViewModel : BaseDomain
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int IdTeam { get; set; }
        public int IdSeason { get; set; }
        public int IdCategory { get; set; }
        public int IdTournament { get; set; }
    }
}
