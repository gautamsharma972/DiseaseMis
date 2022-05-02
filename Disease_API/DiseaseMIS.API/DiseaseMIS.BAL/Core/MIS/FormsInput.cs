using System;
using System.Collections.Generic;
using static DiseaseMIS.BAL.Core.Enums;

namespace DiseaseMIS.BAL.Core.MIS
{
    public class FormsInput
    {
        public string Name { get; set; }
        public FormsType FormType { get; set; }
        public DateTime CreatedDate { get; set; }
        public Districts District { get; set; }
        public Institutes Institute { get; set; }
        public Incharges Incharge { get; set; }
        public List<AnimalsInput> Animals { get; set; }
        public string CurrentStep { get; set; }
        public string Remarks { get; set; }
        public FormsInput()
        {
            this.Animals = new List<AnimalsInput>();
        }
    }

    public class AnimalsInput
    {
        public Guid Id { get; set; }
        public Disease Disease { get; set; }
        public List<SymptomsInput> Symptoms { get; set; }
    }

    public class SymptomsInput
    {
        public Symptoms Symptom { get; set; }
        public Guid Id { get; set; }
        public int Value { get; set; }
        public int Total { get; set; }
        public List<Symptoms> SubSymptoms { get; set; }
    }
}
