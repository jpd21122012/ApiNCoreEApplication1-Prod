namespace ApiNCoreEApplication1.Entity
{
    public class Player : BaseEntity
    {
        public int IdTeam { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Number { get; set; }
        public string Image { get; set; }
        public string BirthDate { get; set; }
        public string CURP { get; set; }
        public bool IsLeader { get; set; }
        public string Address { get; set; }
        public float Phone { get; set; }
        public string Email { get; set; }
    }
}
