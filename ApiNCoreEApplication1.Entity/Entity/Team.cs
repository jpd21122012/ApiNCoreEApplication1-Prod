namespace ApiNCoreEApplication1.Entity
{
    public class Team : BaseEntity
    {
        public int IdCategory { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
}
