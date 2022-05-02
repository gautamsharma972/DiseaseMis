using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiseaseMIS.BAL.Core
{
    public class RecentActions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(36)]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
