using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Prefix
    {
        [Key]
        public int PrefixID { get; set; }
        [Required]
        [Display(Name ="Prefix")]
        public string PrefixName { get; set; }
    }
}
