using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Nationality
    {
        [Key]
        public int NationalityID { get; set; }
        [Required]
        [Display(Name ="Nationality")]
        public string NationalityName { get; set; }
    }
}
