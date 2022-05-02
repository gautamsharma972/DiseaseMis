using System;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core.MIS
{
    public class AnimalCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Animals> Animals { get; set; }
        public int Total
        {
            get
            {
                return this.Animals.Count;
            }
        }
        public int NoOfCases { get; set; }
    }
}