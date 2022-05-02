using System;
using System.Collections.Generic;

namespace DiseaseMIS.BAL.Core.MIS
{
    public class LaboratoryFormsInput
    {
        public List<AnimalCategoryInputs> Categories { get; set; }
        public DateTime CreatedDate { get; set; }
        public Districts District { get; set; }
        public string Remarks { get; set; }

    }

    public class AnimalCategoryInputs
    {
        public Guid Id { get; set; }
        public List<TestTypesInputs> Tests { get; set; }

    }

    public class TestTypesInputs
    {
        public TestTypes TestType { get; set; }
        public Guid Id { get; set; }
        public Samples Sample { get; set; }
        public int TotalValue { get; set; }
        public int NoOfPositiveCases { get; set; }
    }
}
