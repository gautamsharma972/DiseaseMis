using System;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core.MIS
{
    public class DiseaseForms : BaseClass
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Districts District { get; set; }
        public Institutes Institute { get; set; }
        public Incharges Incharge { get; set; }
        public virtual ICollection<FormDiseaseValues> FormDiseaseValues { get; set; }
        public bool IsLocked { get; set; }
        public string CurrentStep { get; set; }
        public string Remarks { get; set; }
        public string FormName { get; set; }
        public DiseaseForms()
        {
            FormDiseaseValues = new List<FormDiseaseValues>();
            IsLocked = false;
        }
    }

    public class FormDiseaseValues
    {
        public Enums.FormsType ValueOfType { get; set; }
        public Guid Id { get; set; }
        public Disease Disease { get; set; }
        public Symptoms Symptom { get; set; }
        public Animals Animal { get; set; }
        public int Value { get; set; }
        public DiseaseForms DiseaseForms { get; set; }

    }

}
