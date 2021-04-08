namespace ApiNCoreEApplication1.Entity
{
    public class PlayerStat : BaseEntity
    {
        public int IdPlayer { get; set; }
        public int IdSeason { get; set; }
        public int IdTournament { get; set; }
        public int IdMatch { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
