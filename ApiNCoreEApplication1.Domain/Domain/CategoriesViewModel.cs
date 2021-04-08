namespace ApiNCoreEApplication1.Domain
{
    public class CategoriesViewModel : BaseDomain
    {
        public int IdSeason { get; set; }
        public int IdTournament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
