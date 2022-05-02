using System;
using System.Collections.Generic;
using static DiseaseMIS.BAL.Core.Enums;

namespace DiseaseMIS.BAL.Core
{
    public class Reports : BaseClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ReportingDate { get; set; }
        public Districts District { get; set; }
        public Institutes Institute { get; set; }
        public Incharges Incharge { get; set; }
        public virtual ICollection<ReportCases> Cases { get; set; }
        public string Remarks { get; set; }
        public string CurrentProcessLink { get; set; }
        public Guid UserId { get; set; }
        public DateTime LastEdited { get; set; }
        public DateTime LastApproved { get; set; }
        public ReportFlags Status { get; set; }
    }

    public class ReportCases
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DiseaseType DiseaseType { get; set; }
        public virtual ICollection<Disease> Disease { get; set; }
    }
}
