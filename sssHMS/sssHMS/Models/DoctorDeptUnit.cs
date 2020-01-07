using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class DoctorDeptUnit
    {
        [Key]
        public int DoctorDeptUnitID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public Doctor Doctor { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }
        
        [Required]
        public int UnitID { get; set; }

        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }
        

    }
}
