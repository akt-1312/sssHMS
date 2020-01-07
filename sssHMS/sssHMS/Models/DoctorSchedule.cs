using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class DoctorSchedule
    {
        [Key]
        public int DoctorScheduleId { get; set; }

        public int DoctorID { get; set; }

        public string DoctorName { get; set; }

        public int DepartmentID { get; set; }
        
        [Display(Name ="Dept: Name")]
        public string DepartmentName { get; set; }

        public int UnitID { get; set; }

        public string UnitName { get; set; }

        public string WeekDay { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        public int PerPatientTime { get; set; }

        //public List<DoctorSchedule> DoctorSchedules { get; set; }

    }
}
