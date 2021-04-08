namespace ApiNCoreEApplication1.Entity
{
    public class Category : BaseEntity
    {

        public int IdSeason { get; set; }
        public int IdTournament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
