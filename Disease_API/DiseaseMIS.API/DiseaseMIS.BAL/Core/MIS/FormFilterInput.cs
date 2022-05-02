using System;

namespace DiseaseMIS.BAL.Core.MIS
{
    public class FormFilterInput
    {
        public DateTime? CreatedDate { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? InstituteId { get; set; }
        public Guid? InchargeId { get; set; }
        public Enums.FormsType FormType { get; set; }
        public string FormName { get; set; }
    }
}
