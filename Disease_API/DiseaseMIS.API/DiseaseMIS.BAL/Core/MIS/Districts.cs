using System;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core
{
    public class Districts : BaseClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Institutes> Institutes { get; set; }
        public virtual ICollection<Laboratory> Laboratories { get; set; }

        public Districts()
        {
            this.Institutes = new List<Institutes>();
            this.Laboratories = new List<Laboratory>();
        }
    }
}
