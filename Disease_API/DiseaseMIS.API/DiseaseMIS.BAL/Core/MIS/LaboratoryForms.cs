using System;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core.MIS
{
    public class LaboratoryForms : BaseClass
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Districts District { get; set; }
        public virtual ICollection<LabFormValues> LabFormValues { get; set; }
        public bool IsLocked { get; set; }
        public string Remarks { get; set; }
        public LaboratoryForms()
        {
            this.LabFormValues = new List<LabFormValues>();
            IsLocked = false;
        }
    }

    public class LabFormValues
    {
        public Guid Id { get; set; }
        public Samples Samples { get; set; }
        public TestTypes TestTypes { get; set; }
        public AnimalCategory AnimalCategory { get; set; }
        public int TotalValue { get; set; }
        public int NoOfPositiveCases { get; set; }
        public LaboratoryForms LaboratoryForms { get; set; }
    }
}
