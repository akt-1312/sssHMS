using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class PatientType
    {
        [Key]
        public int PatientTypeID { get; set; }
        [Required]
        [Display(Name ="PatientType")]
        public string PatientTypeName { get; set; }
    }
}
