using System;
namespace ApiNCoreEApplication1.Entity
{
    public class Season : BaseEntity
    {
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
