using System;
namespace ApiNCoreEApplication1.Domain
{
    public class SeasonViewModel : BaseDomain
    {
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
