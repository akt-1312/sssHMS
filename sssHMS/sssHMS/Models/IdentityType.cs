using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class IdentityType
    {
        [Key]
        public int IdentityTypeID { get; set; }

        [Required]
        [Display(Name ="Identity Type")]
        public string IdentityTypeName { get; set; }
    }
}
