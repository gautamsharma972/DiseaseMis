using System;

namespace DiseaseMIS.BAL.Core
{
    public class Incharges : BaseClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public Enums.Status Status { get; set; } = Enums.Status.Active;
        public Institutes Institute { get; set; }
    }
}
