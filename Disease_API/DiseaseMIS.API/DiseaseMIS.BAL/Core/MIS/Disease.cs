using System;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core
{
    public class Disease : BaseClass
    {
        public Guid Id { get; set; }
        public DiseaseType DiseaseType { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Symptoms> Symptoms { get; set; }
        public Disease()
        {
            this.Symptoms = new List<Symptoms>();
        }
        public int ListingOrder { get; set; }
    }

    public class DiseaseType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class Symptoms
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Disease Disease { get; set; }
        public virtual ICollection<Symptoms> SubSymptoms { get; set; }
        public Symptoms()
        {
            this.SubSymptoms = new List<Symptoms>();
        }
    }
}
