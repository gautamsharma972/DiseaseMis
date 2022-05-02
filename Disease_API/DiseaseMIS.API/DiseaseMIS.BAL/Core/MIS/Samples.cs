using System;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core.MIS
{
    public class Samples : BaseClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TestTypes> TestTypes { get; set; }
        public Samples()
        {
            this.TestTypes = new List<TestTypes>();
        }
        public int ListingOrder { get; set; }
    }

    public class TestTypes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Samples Sample { get; set; }
        public virtual ICollection<TestTypes> SubTests { get; set; }
    }
}
