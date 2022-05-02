using System;

namespace DiseaseMIS.BAL.Core.MIS
{
    public class Animals : BaseClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AnimalCategory Category { get; set; }
        public int ListingOrder { get; set; }
    }
}
