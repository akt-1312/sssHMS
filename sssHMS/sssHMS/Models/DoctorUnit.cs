using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class DoctorUnit
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public Doctor Doctor { get; set; }

        [Required]
        public int UnitID { get; set; }

        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }
    }
}
