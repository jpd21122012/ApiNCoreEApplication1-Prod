namespace ApiNCoreEApplication1.Domain
{
    public class TeamsViewModel : BaseDomain
    {
        public int IdCategory { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
}
