using sssHMS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sssHMS.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        
        public string GenCode { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string PhoneNumber1 { get; set; }

        public string PhoneNumber2 { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

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
       
        public string UnitName { get; set; }

        public int StatusID { get; set; }

        [ForeignKey("StatusID")]
        public Status Status { get; set; }

        public int BloodID { get; set; }

        [ForeignKey("BloodID")]
        public Blood Blood { get; set; }

        public string Gender { get;set; }

        public ICollection<DoctorUnit> DoctorUnits { get; set; }

        public ICollection<DoctorDeptUnit> DoctorDeptUnits { get; set; }

        public Doctor()
        {
            DoctorUnits = new List<DoctorUnit>();
        }
    }
}
