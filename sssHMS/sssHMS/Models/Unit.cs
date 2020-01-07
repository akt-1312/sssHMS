using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }
        [Required]
        [Display(Name = "Unit")]
        public string UnitName { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }        

    }
}
