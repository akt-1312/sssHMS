using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Gender
    {
        [Key]
        public int GenderID { get; set; }
        [Required]
        [Display(Name ="Gender")]
        public string GenderName { get; set; }
    }
}
