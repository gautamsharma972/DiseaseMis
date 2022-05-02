using System;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core
{
    public class Institutes : BaseClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Incharges> Incharges { get; set; }
        public Districts District { get; set; }

        public Institutes()
        {
            this.Incharges = new List<Incharges>();
        }
    }
}
