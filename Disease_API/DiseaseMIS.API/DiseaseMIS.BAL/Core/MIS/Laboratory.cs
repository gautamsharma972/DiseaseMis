using System;

namespace DiseaseMIS.BAL.Core
{
    public class Laboratory : BaseClass
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public Districts Districts { get; set; }
    }
}
