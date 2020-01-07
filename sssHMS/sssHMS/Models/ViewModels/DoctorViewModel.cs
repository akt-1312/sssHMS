using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models.ViewModels
{
    public class DoctorViewModel
    {

        public string GenCode { get; set; }
        [Key]
        public int DoctorID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Image { get; set; }
        public DateTime DOB { get; set; }

        public DateTime? JobStart { get; set; }
        public DateTime? JobEnd { get; set; }
        public string DescriptionforLeave { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public int DesignationID { get; set; }
        [ForeignKey("DesignationID")]
        public Designation Designation { get; set; }
        public int DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }
      
        public string UnitName { get; set; }
        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public Status Status { get; set; }
        public int BloodID { get; set; }
        [ForeignKey("BloodID")]
        public Blood Blood { get; set; }
        public string Gender { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Unit> Units { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public List<Status> Statuses { get; set; }

        public List<DoctorUnitViewModel> UnitList { get; set; }
    }
}
