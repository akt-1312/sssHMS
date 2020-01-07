using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Designation
    {
        [Key]
        public int DesignationID { get; set; }
        [Required]
        [Display(Name ="Designation")]
        public string DesignationName { get; set; }
        
    }
}
