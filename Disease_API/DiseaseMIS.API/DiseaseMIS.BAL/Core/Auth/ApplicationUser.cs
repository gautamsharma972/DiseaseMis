using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiseaseMIS.BAL.Core
{
    public class ApplicationUser : IdentityUser
    {
        public string Location { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [NotMapped]
        public List<string> UserRoles { get; set; }

    }
}
