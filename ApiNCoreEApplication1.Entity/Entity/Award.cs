namespace ApiNCoreEApplication1.Entity
{
    /// <summary>
    /// An Award object
    /// </summary>
    public class Award : BaseEntity
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
