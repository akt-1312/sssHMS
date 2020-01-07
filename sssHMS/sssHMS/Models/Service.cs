using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }

        [Required]
        [Display(Name ="Service Name")]
        public string ServiceName { get; set; }
    }
}
