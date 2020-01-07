using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class MaritalStatus
    {
        [Key]
        public int MaritalStatusID { get; set; }
        [Required]
        [Display(Name ="MaritalStatus")]
        public string MaritalStatusName { get; set; }
    }
}
